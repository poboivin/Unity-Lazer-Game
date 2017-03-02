using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class wallrun : MonoBehaviour {
	private RigidbodyFirstPersonController cc;
	private Rigidbody rb;
	public bool isWallR = false;
	public bool isWallL = false;
	private RaycastHit hitR;
	private RaycastHit hitL;
	public int jumpCount = 0;

	public float runTime = 0.5f;
	public Transform cameraHolder;
	public float rotateAngle = 12;
	void Start()
	{
		cc = GetComponent<RigidbodyFirstPersonController>();
		rb = GetComponent<Rigidbody>();
		cc.advancedSettings.airControl = false;
	}

	void Update()
	{
		if (cc.Grounded) 
		{
			jumpCount = 0;
		} 
		else if ((Input.GetKey (KeyCode.A)) && jumpCount < 1 && !isWallL && !isWallR) {
			if (Physics.Raycast (transform.position, -transform.right, out hitL, 1)) {
				if (hitL.transform.tag == "Wall") {
					isWallL = true;
					isWallR = false;
					jumpCount += 1;
					rb.useGravity = false;
						cameraHolder.Rotate(new Vector3 (0,0,-rotateAngle));
					StartCoroutine (afterRun ());
				}
			}

		} 
		else if ((Input.GetKey (KeyCode.D)) && jumpCount < 1 && !isWallL && !isWallR) 
		{
			if (Physics.Raycast (transform.position, transform.right, out hitR, 1)) {
				if (hitR.transform.tag == "Wall") {
					isWallR = true;
					isWallL = false;
					jumpCount += 1;
					rb.useGravity = false;
					cameraHolder.Rotate(new Vector3 (0,0,rotateAngle));
					StartCoroutine (afterRun ());
				}
			}
		}
	}
	void FixedUpdate(){
		if (CrossPlatformInputManager.GetButtonDown("Jump") &&( isWallL || isWallR))
		{
			rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
			Vector3 dir = -transform.right;
			if(isWallL)
				dir = transform.right;
				
			rb.AddForce((dir * cc.movementSettings.JumpForce ), ForceMode.Impulse);
			rb.AddForce(new Vector3(0f, cc.movementSettings.JumpForce, 0f), ForceMode.Impulse);
			jumpCount = 0;
		}
	
	}
	IEnumerator afterRun()
	{
		
		yield return new WaitForSeconds(runTime);
		if (isWallL) {
			cameraHolder.Rotate (new Vector3 (0, 0, rotateAngle));
		}
		if (isWallR) {
			cameraHolder.Rotate(new Vector3 (0,0,-rotateAngle));

		}
		isWallR = false;
		isWallL = false;
		rb.useGravity = true;
		//cameraHolder.rotation = Quaternion.Euler(Vector3.zero);

	}
}﻿