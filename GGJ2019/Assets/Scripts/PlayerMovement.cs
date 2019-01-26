using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float vertical;
	public float horizontal;
	public float MovementSpeed;
	CharacterController characterController;
	CameraController cameraController;
	public Animator anim;
	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
		cameraController = GetComponentInChildren<CameraController>();
		anim = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update () {
		GetInputs();
		Move();
	}

	void GetInputs(){
		vertical = Input.GetAxis("Vertical");	
		horizontal = Input.GetAxis("Horizontal");	
	}

	void Move(){
		
		Vector3 direction = cameraController.transform.rotation * new Vector3(horizontal, 0f, vertical);
		characterController.Move(direction * MovementSpeed * Time.deltaTime);

		anim.SetFloat("Vertical",direction.z);
		anim.SetFloat("Horizontal",direction.x);
	}
}
