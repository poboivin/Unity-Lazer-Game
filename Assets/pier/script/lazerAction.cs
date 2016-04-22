using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]
public class lazerAction : MonoBehaviour, IAction
{
    public float range = 6;
    public int maxBounce = 3;
    LineRenderer line;
    public Material lineMaterial;
    bool active = false;
    bool temp = false;
	// Use this for initialization
	void Start()
    {
        line = GetComponent<LineRenderer>();
        line.SetVertexCount(2);
        line.material = lineMaterial;
        line.enabled = false;
    }


    // LateUpdate is called once per frame after all update call
    void LateUpdate()
    {
        if (active)
        {
            Ray ray;

            ray = new Ray(transform.position, transform.forward);

            LazerManager.updateRay(line, ray, range, maxBounce, 0);
            line.enabled = true;
            if (temp)
            {
                temp = false;
                active = false;
            }
        
        }
        else 
        {
            line.enabled = false;
        }

    }

    public bool OnStartAction()
    {

        active = true;

        return true;
    }

    public bool OnUpdateAction()
    {
        temp = true;
        active = true;
        return true;
    }

    public bool OnExitAction()
    {

        active = false;
        return true;
    }
}
