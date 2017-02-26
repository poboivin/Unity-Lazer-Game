using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookTest : MonoBehaviour {
    Rigidbody rb;
    public Transform hp;
    Vector3 hookPoint;
    public float ropeLength = 5f;
    LineRenderer lr;
    public bool selfInit = false;
    public bool retract = false;
    float speed = 3f;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponentInParent<Rigidbody>();
        lr = GetComponentInParent<LineRenderer>();
        hookPoint = hp.position;
        if (selfInit)
            ropeLength = Vector3.Distance(hookPoint, transform.position);

        rb.AddForce(transform.forward * 10f, ForceMode.VelocityChange);
    }
    float sign(float a)
    {
        if(a < 0)
        {
            return -1;
        }
        if (a > 0)
            return 1;

        return 0;

    }
    void Update()
    {
       
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, hookPoint);
    }
    // Update is called once per frame
    void FixedUpdate() {
       
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

            float tensionForce = rb.mass *Physics.gravity.magnitude * Mathf.Cos(Mathf.Deg2Rad * inclinationAngle);
            float centripetalForce = ((rb.mass * Mathf.Pow(currentVelocity.magnitude, 2)) / ropeLength);
          //  tensionForce += centripetalForce;

           // currentVelocity += Tensiondir * tensionForce * Time.fixedDeltaTime;
            rb.AddForce(Tensiondir * 10f, ForceMode.Force);
            //Debug.Log("t");
        }
        if (retract)
        {
            if (ropeLength > 1)
                ropeLength -= speed * Time.fixedDeltaTime;
        }
    }
}
