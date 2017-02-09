using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]
public class lazer : MonoBehaviour {
    Vector2 mouse;

    LineRenderer line;
    public Material lineMaterial;
    public Transform flashSprite;
    public Transform hitSprite;
    public bool isPlayerWeapon;
    public float range =  6;
    public int maxBounce=3;
   public bool HookEnabled = false;
    float grappleDist = 0;
   public bool grappleHook = false;
    Vector3 hookPoint;
    Rigidbody rb;
	// Use this for initialization
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        line.numPositions =(2);
        line.material = lineMaterial;
        line.enabled = false;
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire2"))
        {
            line.enabled = true;
        }
        else 
        {
            line.enabled = false;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            HookEnabled = true;
        }
        else
        {
            HookEnabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            grappleHook = false;
        }
     
        if (line.enabled)
        {
            Ray ray;

           
                ray = (Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f))); // ray to center of screen
                ray.origin = transform.position;

            if (HookEnabled && grappleHook == false)
                grapple(ray);


            LazerManager.updateRay(line, ray, range, maxBounce, 0);
          
        }
	}
 void grapple(Ray ray)
    {
     

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            grappleHook = true;
            grappleDist = hit.distance;

            hookPoint = hit.point;

            //Vector3 relativePos = transform.position - hookPoint;
            //if (relativePos.z != 0)
            //{
            //    Debug.Log(relativePos.z);
               //rb.AddForce(relativePos.normalized * -50, ForceMode.Acceleration);
            //}

        }

    }

}
