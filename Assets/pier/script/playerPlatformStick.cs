using UnityEngine;
using System.Collections;

public class playerPlatformStick : MonoBehaviour {
    public string TriggerTag = "Player";
    public void OnTriggerEnter(Collider other)
    {
        if (  other.gameObject.tag == TriggerTag)
        {
            other.gameObject.transform.SetParent(this.transform);
        }
    }
  
    public void OnTriggerExit(Collider other)
    {
        if (  other.gameObject.tag == TriggerTag)
        {
            other.gameObject.transform.SetParent(null);
        }
    }
    
}
