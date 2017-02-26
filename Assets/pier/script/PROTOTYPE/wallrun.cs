using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class wallrun : MonoBehaviour {

	public bool isWallR = false;
	public bool isWallL = false;
	private RaycastHit hitR;
	private RaycastHit hitL;
	public int jumpCount = 0;
	private RigidbodyFirstPersonController cc;
	private Rigidbody rb;
	public float runTime = 0.5f;

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
		if (Input.GetKeyDown(KeyCode.E) && jumpCount <= 1)
		{
			if (Physics.Raycast(transform.position, -transform.right, out hitL, 1))
			{
				if (hitL.transform.tag == "Wall")
				{
					isWallL = true;
					isWallR = false;
					jumpCount += 1;
					rb.useGravity = false;
					StartCoroutine(afterRun());
				}
			}
			if (Physics.Raycast(transform.position, transform.right, out hitR, 1))
			{
				if (hitR.transform.tag == "Wall")
				{
					isWallR = true;
					isWallL = false;
					jumpCount += 1;
					rb.useGravity = false;
					StartCoroutine(afterRun());
				}
			}
		}
	}
	IEnumerator afterRun()
	{
		yield return new WaitForSeconds(runTime);
		isWallR = false;
		isWallL = false;
		rb.useGravity = true;
	}
}﻿