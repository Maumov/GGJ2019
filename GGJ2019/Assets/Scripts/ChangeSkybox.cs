using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour {

	public Material skybox;
	public Cubemap day, night;

	void OnTriggerEnter(Collider other){
		if(other.name.Contains("Player")){
			skybox.SetTexture("_Tex", day);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.name.Contains("Player")){
			skybox.SetTexture("_Tex", night);
		}
	}
}
