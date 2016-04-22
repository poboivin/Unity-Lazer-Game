using UnityEngine;
using System.Collections;

public class paintGun : MonoBehaviour {
    public Transform splatter;
    public float force = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Transform splat = Instantiate(splatter, transform.position, transform.rotation) as Transform;

            splat.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);
        }
	}
}
