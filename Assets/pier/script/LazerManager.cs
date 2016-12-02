using UnityEngine;
using System.Collections;

public class LazerManager : MonoBehaviour 
{

    public Transform glowPrefab;
    public static void updateRay(LineRenderer line,Ray Rray, float range,int maxBounce ,int numBounce)
    {
        RaycastHit hit;
       
        line.numPositions = (numBounce + 2);
      //  line.sortingOrder = 3;
        if (Physics.Raycast(Rray, out hit, range,  Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {

            line.SetPosition(numBounce, Rray.origin);  //sets the line position
            line.SetPosition(numBounce + 1, hit.point);

            ILazerAction[] ObjectToActOn = hit.collider.gameObject.GetComponentsInChildren<ILazerAction>();
            if (ObjectToActOn.Length > 0)
            {
               foreach(ILazerAction obj in ObjectToActOn){
                   obj.OnUpdateAction();
               }
                
            }
            else if (hit.collider.gameObject.tag == "glass")
            {
                Ray ray = new Ray(hit.point, Rray.direction);

                updateRay(line, ray, range, maxBounce, numBounce +1);
             
            }
            else if (numBounce < maxBounce)
            {
                Ray ray = new Ray(hit.point, Vector3.Reflect(Rray.direction, hit.normal));
               
                updateRay(line, ray, range, maxBounce, numBounce + 1);
                return;
            }
        }
        else
        {
            //if we havent hit just put the end of the line at max range
            line.SetPosition(numBounce, Rray.origin);
            line.SetPosition(numBounce + 1, Rray.GetPoint(range));
        }
    }
}
