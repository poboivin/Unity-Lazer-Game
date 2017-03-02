using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookV2 : MonoBehaviour {
	LineRenderer line;
	public float range = 6;
	public bool grappleHook = false;
	Vector3 hookPoint;
	public float ropeLength = 5f;
	public  Rigidbody rb;
	public float Retactspeed = 3f;
	public float HookForce= 50;	// Use this for initialization
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
			rb.useGravity = true;
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
	void HookPhysics()
	{
		 
		float distanceAfterGravity = Vector3.Distance(hookPoint, transform.position);
		rb.useGravity = false;
		if(distanceAfterGravity > 3)
		{
			Vector3 Tensiondir = hookPoint - transform.position;

		rb.AddForce(Tensiondir * HookForce, ForceMode.Force);
			//Debug.Log("t");
		}

	}
}
