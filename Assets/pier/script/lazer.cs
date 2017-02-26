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

    // Use this for initialization
    void Start()
    {
        
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
       
      
     
        if (line.enabled)
        {
            Ray ray;

           
                ray = (Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f))); // ray to center of screen
                ray.origin = transform.position;

            


            LazerManager.updateRay(line, ray, range, maxBounce, 0);
          
        }
	}

    

}
