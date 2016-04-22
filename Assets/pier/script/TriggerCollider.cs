using UnityEngine;
using System.Collections;

public class TriggerCollider : MonoBehaviour,IhasState {
    public string TriggerTag = "Player";
	// Use this for initialization
    public bool OnEnter = true;
    public bool OnStay = true;
    public bool OnExit = true;
    public GameObject target;
    private bool isActive = false;
    
    IAction ObjectToActOn;
    public bool getState()
    {
        return isActive;
    }
    public void Start()
    {
        if (target != null)
            ObjectToActOn = target.GetComponentInChildren<IAction>();

    }
    public void OnTriggerEnter(Collider other)
    {
        if (OnEnter && other.gameObject.tag == TriggerTag)
        {
            if (ObjectToActOn != null)
                ObjectToActOn.OnStartAction();
            
            isActive = true;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (OnStay && other.gameObject.tag == TriggerTag )
        {
            if (ObjectToActOn != null)
                ObjectToActOn.OnUpdateAction();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (OnExit && other.gameObject.tag == TriggerTag)
        {
            if (ObjectToActOn != null)
                ObjectToActOn.OnExitAction();
            
            isActive = false;
        }
    }
    
}
