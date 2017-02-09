using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlePuzzle : MonoBehaviour, Iinteractable
{
    public int targetRotation = 0;
    public bool isRorating = false;
    private IEnumerator rotateCoroutine()
    {

        isRorating = true;
        while (Mathf.RoundToInt(transform.rotation.eulerAngles.z) != targetRotation)
        {
            transform.Rotate(Vector3.forward, 1);
            yield return null;
        }
        isRorating = false;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, targetRotation));
        yield return true;
    }

    
    public bool OnExitAction()
    {
        throw new NotImplementedException();
    }

    public bool OnStartAction()
    {

        if(isRorating == false)
        {
            
            targetRotation += 120;
            if(targetRotation == 360)
            {
                targetRotation = 0;
            }
            StartCoroutine(rotateCoroutine());
            return true;
        }
        else
        {

            return false;
        }
       
    }

    public bool OnUpdateAction()
    {
        throw new NotImplementedException();
    }

    
}
