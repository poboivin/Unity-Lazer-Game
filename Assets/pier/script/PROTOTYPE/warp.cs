using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warp : MonoBehaviour {

    public Transform warpTarget;
    public CustomImageEffect warpEffect;
    public float range = 6;
   public float warpDelay = 1;
    // Use this for initialization
    void Start () {
		
	}
	void StartWarp()
    {
        warpEffect.startWarp();
        Invoke("endWarp", warpDelay);        
    }
    void endWarp()
    {
        this.transform.position = warpTarget.position;

    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.G))
        {
            warpTarget.gameObject.SetActive(true);
            Ray ray;
            ray = (Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f))); // ray to center of screen
            ray.origin = transform.position;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, range, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
            {
                warpTarget.position = hit.point;
            }
            else
            {
                warpTarget.position = ray.GetPoint(range);

            }
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            warpTarget.gameObject.SetActive(false);
            StartWarp();
        }

	}
}
