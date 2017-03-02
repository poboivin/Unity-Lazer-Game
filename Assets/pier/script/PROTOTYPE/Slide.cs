using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;


public class Slide : MonoBehaviour {
	private Rigidbody rb;
	private RigidbodyFirstPersonController cc;
	public Transform cam;
	public bool slide = false;
	public bool sliding = false;

	public float slidePower = 12;	
	public float slideTime = 0.5f;


	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		cc = GetComponent<RigidbodyFirstPersonController>();
	}

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.LeftControl) && sliding  == false)
		{
			slide = true;
		}

	}
	IEnumerator StartSlide()
	{
		Vector3 desiredMove = cam.transform.forward;
		desiredMove = Vector3.ProjectOnPlane (desiredMove, cc.GroundContactNormal).normalized;
		//rb.WakeUp ();
		sliding = true;
		float timer = 0;
		while (true) 
		{
			rb.AddForce(desiredMove * slidePower, ForceMode.Impulse);

			timer += Time.deltaTime;
			if (timer >= slideTime) {
				break;
			}
			if (CrossPlatformInputManager.GetButtonDown ("Jump")) {
				break;
			}
			yield return new WaitForFixedUpdate();
		}
		sliding = false;
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		if (slide && cc.Grounded) 
		{
			StartCoroutine (StartSlide ());
	
		}
		slide = false;
	}
}
