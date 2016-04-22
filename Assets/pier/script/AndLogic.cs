using UnityEngine;
 
using System.Collections;
using System.Collections.Generic;
public class AndLogic : MonoBehaviour {

    public GameObject[] Actiontargets;
    List<IAction> ObjectsToActOn;

    public GameObject[] Conditiontargets;
    List<IhasState> ObjectsToCheckOn;
	// Use this for initialization

    bool JustActive = true;
    bool isActive = false;


    public bool OnEnter = true;
    public bool OnStay = true;
    public bool OnExit = true;

	void Start () {
        ObjectsToActOn = new List<IAction>();
        ObjectsToCheckOn = new List<IhasState>();

        foreach (GameObject obj in Conditiontargets)
        {
            IhasState temp = obj.GetComponentInChildren<IhasState>();
            if (temp != null)
            {
                ObjectsToCheckOn.Add(temp);
            }
        }

        foreach (GameObject obj in Actiontargets)
        {
            IAction temp = obj.GetComponentInChildren<IAction>();
            if (temp != null)
            {
                ObjectsToActOn.Add(temp);
            }
        }
	}
	
	// Update is called once per frame
    void LateUpdate()
    {
        isActive = true;
        foreach (IhasState obj in ObjectsToCheckOn)
        {
            if (obj.getState() == false)
            {
                isActive = false;
            }
        }

        if (isActive)
        {
            //check if this is the first time we activated
            if (JustActive)
            {
                JustActive = false;
                //check if we need to call startFunc
                if (OnEnter)
                {
                    foreach (IAction obj in ObjectsToActOn)
                    {
                        obj.OnStartAction();
                    }            
                }
            }
            //check if we need to call update func
            if (OnStay)
            {
                foreach (IAction obj in ObjectsToActOn)
                {
                    obj.OnUpdateAction();
                }
            }
            
          
        }
        //check if we are no longer active and that we have activated at least one frame
        if (isActive == false && JustActive == false)
        {
            JustActive = true;
            //check if we need to call exit func
            if (OnExit)
            {
                foreach (IAction obj in ObjectsToActOn)
                {
                    obj.OnExitAction();
                }
            }

        }
	}
}
