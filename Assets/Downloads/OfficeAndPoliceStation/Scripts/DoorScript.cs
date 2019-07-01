﻿using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public bool open = false;
	public float doorOpenAngle = 90f;
	public float doorCloseAngle = 0f;
	public float smooth = 2f;

	void Start () 
	{
		
	}
	

	public void ChangeDoorState()
	{
		open = !open;
		GetComponent<AudioSource>().Play ();
	}

	void Update () 
	{
		if(open) //open == true
		{
			Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
		}
		else
		{
			Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smooth * Time.deltaTime);
		}
	}
}
