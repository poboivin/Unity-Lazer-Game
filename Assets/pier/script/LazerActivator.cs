using UnityEngine;
using System.Collections;

public class LazerActivator : MonoBehaviour, ILazerAction, IhasState
{

    public Color inactiveColor;
    public Color activeColor;

    public GameObject target;
    bool JustActive = true;
    bool isActive = false;

    Renderer rend;
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

        rend = transform.GetComponent<Renderer>();
        inactiveColor = rend.material.color;
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
            
            rend.material.color = inactiveColor;
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
            rend.material.color = activeColor;
        }
        
        if (OnStay)
        {
            if (ObjectToActOn != null)
                ObjectToActOn.OnUpdateAction();
        }
        isActive = true;
        return true;
    }

    void OnDisable()
    {
        Destroy(rend.material); // destroy instance material
    }
    
}
