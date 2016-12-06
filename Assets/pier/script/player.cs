using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    public float range = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
          //  Debug.Log("test");
            RaycastHit hit;
            Ray ray;
            ray = (Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f))); // ray to center of screen
            ray.origin = transform.position;
            if (Physics.Raycast(ray, out hit, range, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
            {
                if (hit.rigidbody != null)
                {
                    Iinteractable obj = hit.rigidbody.gameObject.GetComponent<Iinteractable>();
                   
                    if (obj != null)
                    {
                        obj.OnStartAction();
                       
                    }
                }
            }
        }
    }
}
