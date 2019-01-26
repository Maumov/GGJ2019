using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour {
	Animator anim;
	public Transform left, right;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	void OnAnimatorIK(){
		anim.SetIKPositionWeight(AvatarIKGoal.LeftHand,1f);
		anim.SetIKRotationWeight(AvatarIKGoal.LeftHand,1f);
		anim.SetIKPositionWeight(AvatarIKGoal.RightHand,1f);
		anim.SetIKRotationWeight(AvatarIKGoal.RightHand,1f);
		anim.SetIKPosition(AvatarIKGoal.RightHand,right.position);
		anim.SetIKRotation(AvatarIKGoal.RightHand,right.rotation);
		anim.SetIKPosition(AvatarIKGoal.LeftHand,left.position);
		anim.SetIKRotation(AvatarIKGoal.LeftHand,left.rotation);
	}	
	// Update is called once per frame
	void Update () {
		
	}
	void FootL(){
		
	}
	void FootR(){

	}
}
