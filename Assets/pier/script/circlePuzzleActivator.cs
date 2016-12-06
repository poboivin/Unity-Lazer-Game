using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlePuzzleActivator : MonoBehaviour, Iinteractable
{
    [System.Serializable]
    public class puzzlePiece: System.Object
    {
       public circlePuzzle test;
       public float targetRoration;
  
    }

    public puzzlePiece[] pieces;
    public GameObject target;
    IAction ObjectToActOn;
    
    public void Start()
    {

        if (target != null)
            ObjectToActOn = target.GetComponent<IAction>();

    }
    public bool OnExitAction()
    {
        return true;
    }

    public bool OnStartAction()
    {
      
        bool Unlock = true;
        foreach(puzzlePiece p in pieces)
        {
            if( checkMatch(p) == false)
            {
                Unlock = false;
                return false;
            }

        }
        if (ObjectToActOn != null)
            ObjectToActOn.OnStartAction();
       // Debug.Log(" d ");
        return true;

    }
    bool checkMatch(puzzlePiece p)
    {
        if(p.test.targetRotation == p.targetRoration)
        {
            return true;
        }
        return false;

    }
    public bool OnUpdateAction()
    {
        return true;
    }

}
