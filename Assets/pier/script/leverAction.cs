using UnityEngine;
using System.Collections;

public class leverAction : MonoBehaviour, IAction, Iinteractable,IhasState
{

    public Animator anim;
    public string conditionString = "isOn";
    public bool state = false;

    public bool getState()
    {
        return state;
    }
    public void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

    }

    public bool OnStartAction()
    {
        if (anim.GetBool(conditionString))
        {
            anim.SetBool(conditionString, false);
            state = false;
        }
        else
        {
            anim.SetBool(conditionString, true);
            state = true;
        }
        return true;
    }

    public bool OnUpdateAction()
    {
        return true;
    }

    public bool OnExitAction()
    {
        return true;
    }
}
