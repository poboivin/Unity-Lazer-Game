using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPick : MonoBehaviour {

    public GameObject Pick;
    public GameObject Lock;
    public float winAngle = 90;
    public float winAngleRange = 10;
    public float TurnZoneFactor = 3f;
    public float LockAngle = 90;
    public float PickAngle = 0;


    //debug transforms
    public GameObject text;
    public Transform winZone1;
    public Transform winZone2;
    public Transform turnZone1;
    public Transform turnZone2;

    void UpdateDebugObj()
    {

        winZone1.transform.rotation = Quaternion.Euler(0, 0, winAngle - winAngleRange);
        winZone2.transform.rotation = Quaternion.Euler(0, 0, winAngle + winAngleRange);
        turnZone1.transform.rotation = Quaternion.Euler(0, 0, winAngle - winAngleRange * TurnZoneFactor);
        turnZone2.transform.rotation = Quaternion.Euler(0, 0, winAngle + winAngleRange * TurnZoneFactor);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            if (
            LockAngle > 85 ||
            LockAngle / 90 > Mathf.Abs(Mathf.DeltaAngle(winAngle, PickAngle)) / (winAngleRange * TurnZoneFactor) ||
            Mathf.Abs(Mathf.DeltaAngle(winAngle, PickAngle)) < winAngleRange
            )
            {
                LockAngle -= 2;
            }
            else
            {
                LockAngle += 1;
            }
        }
        else
        {
            PickAngle += -Input.GetAxis("Mouse X");
            LockAngle += 1;
        }

        if (LockAngle > 90)
        { 
            LockAngle = 90;
        }
        if (LockAngle < 0)
        {
            LockAngle = 0;
            text.SetActive(true);
        }
        Pick.transform.rotation = Quaternion.Euler(0, 0, PickAngle);
        Lock.transform.rotation = Quaternion.Euler(0, 0, LockAngle);

        UpdateDebugObj();
    }
}
