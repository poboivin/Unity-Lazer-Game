using UnityEngine;
using System.Collections;

public class LazerActivator : MonoBehaviour, ILazerAction, IhasState
{
    public GameObject target;
    bool JustActive = true;
    bool isActive = false;


    public bool OnEnter = true;
    public bool OnStay = true;
    public bool OnExit = true;
    IAction ObjectToActOn;

    public bool getState()
    {
        return isActive;
    }
    public void Start()
    {
        if (target != null)
            ObjectToActOn = target.GetComponent<IAction>();

    }

    public void Update()
    {
        
        if (JustActive == false &&isActive == false)
        {
            JustActive = true;
            if (OnExit)
            {
                if (ObjectToActOn != null)
                    ObjectToActOn.OnExitAction();
            }
        }
        isActive = false;
    }

    public bool OnUpdateAction()
    {
        if (JustActive)
        {
            JustActive = false;
            if(OnEnter)
            {
                if (ObjectToActOn != null)
                    ObjectToActOn.OnStartAction();
            }
        }
        
        if (OnStay)
        {
            if (ObjectToActOn != null)
                ObjectToActOn.OnUpdateAction();
        }
        isActive = true;
        return true;
    }
}
