using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class Energy : MonoBehaviour {
	public float maxEnergy;
	public float currentEnergy;

	bool usingLight;
	public Light theLight;
	public BipedIK biped;
	// Use this for initialization
	void Start () {
		currentEnergy = maxEnergy;
	}
	
	// Update is called once per frame
	void Update () {
		currentEnergy -= Time.deltaTime + (usingLight? 2f : 1f);
		if(currentEnergy < 0f){
			GameOver();	
		}
		if(Input.GetButtonDown("Fire1")){
			usingLight = !usingLight;
			theLight.enabled = usingLight;
			biped.solvers.rightHand.IKPositionWeight = usingLight? 1f : 0f ;
		}

	}

	public void GameOver(){
		GetComponentInChildren<Animator>().SetTrigger("Death");
	}

	public void BackToMainMenu(){
		
	}
}
