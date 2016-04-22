using UnityEngine;
using System.Collections;

public class doorAction : MonoBehaviour ,IAction
{

    public Animator anim;
    public string conditionString = "isOpen";
    public void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

    }
    public bool OnStartAction()
    {
        anim.SetBool(conditionString, true);
        return true;
    }



    public bool OnUpdateAction()
    {

        return true;
    }

    public bool OnExitAction()
    {

        anim.SetBool(conditionString, false);
        return true;
    }
}
