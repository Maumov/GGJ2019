using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	float MouseX;
	float MouseY;
	public float MouseXSpeed, MouseYSpeed;
	public float minAngle, maxAngle;
	public Transform target;
	Transform   child;
	// Use this for initialization
	void Start () {
		child = GetComponentInChildren<Camera>().transform;
	}
	
	// Update is called once per frame
	void Update () {
		GetInputs();
		Move();
	}

	void GetInputs(){
		MouseY = -Input.GetAxis("Mouse Y");	
		float value = Vector3.SignedAngle(child.forward,transform.forward, transform.right);
		value *= -1f;
		if(MouseY > 0f && value > maxAngle ){
			MouseY = 0f;
		}
		if(MouseY < 0f && value < minAngle ){
			MouseY = 0f;
		}
		MouseX = Input.GetAxis("Mouse X");
	}

	void Move(){
		transform.position = target.transform.position;
		transform.RotateAround(transform.position, Vector3.up, MouseX * MouseXSpeed * Time.deltaTime);
		child.RotateAround(transform.position, transform.right, MouseY * MouseYSpeed * Time.deltaTime);
	}
}
