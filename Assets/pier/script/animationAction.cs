using UnityEngine;
using System.Collections;

public class animationAction : MonoBehaviour, IAction
{
    public enum Modes {switchMode,buttonMode }
    public Modes Mode;
    public string conditionString;

    public bool OnStartAction() 
    { 
        
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
