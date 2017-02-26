using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour {
    LineRenderer line;
    public float range = 6;
    public bool grappleHook = false;
    Vector3 hookPoint;
    public float ropeLength = 5f;
    public  Rigidbody rb;
    public float Retactspeed = 3f;
    // Use this for initialization
    void Start () {
        rb = GetComponentInParent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        line.numPositions = (2);
        line.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            grappleHook = false;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            
            Ray ray;
            ray = (Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f))); // ray to center of screen
            ray.origin = transform.position;

            if (grappleHook == false)
                Hook(ray);
        }

        if (grappleHook)
        {
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hookPoint);
        }
        else
        {
            line.enabled = false;
        }


          
        
    }
    void FixedUpdate()
    {
        if (grappleHook)
        {
            HookPhysics();
        }
    }
    void HookPhysics()
    {
        float gravityForce = Physics.gravity.magnitude * rb.mass;
        Vector3 gravityDirection = Physics.gravity.normalized;

        Vector3 currentVelocity = rb.velocity;

        //calculate where it will be
        Vector3 auxiliaryMovementDelta = currentVelocity * Time.fixedDeltaTime;

        float distanceAfterGravity = Vector3.Distance(hookPoint, transform.position + auxiliaryMovementDelta);

        if (distanceAfterGravity > ropeLength || Mathf.Approximately(distanceAfterGravity, ropeLength))
        {
            Vector3 Tensiondir = hookPoint - transform.position;
            float inclinationAngle = Vector3.Angle(transform.position - hookPoint, gravityDirection);

            float tensionForce = rb.mass * Physics.gravity.magnitude * Mathf.Cos(Mathf.Deg2Rad * inclinationAngle);
            float centripetalForce = ((rb.mass * Mathf.Pow(currentVelocity.magnitude, 2)) / ropeLength);
            tensionForce += centripetalForce;
            float rForce = rb.mass * Retactspeed;
            rb.drag = 0f;
            rb.AddForce(Tensiondir * (tensionForce + rForce), ForceMode.Force);
            //Debug.Log("t");
        }

        if (ropeLength > 3)
            ropeLength -= Retactspeed * Time.fixedDeltaTime;


    }
    void Hook(Ray ray)
    {


        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            grappleHook = true;
            hookPoint = hit.point;
            ropeLength = Vector3.Distance(hookPoint, transform.position);


        }

    }
}
