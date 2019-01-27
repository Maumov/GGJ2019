using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour {

	public Material skybox;
	public Light light;
	public Color day, night;
	public Color sunLight, moonLight;

	bool Tonight; 

	void Update(){
		if(Tonight){
			Color c = Vector4.Lerp(skybox.GetColor("_Tint"), night, Time.deltaTime);
			skybox.SetColor("_Tint", c);
			Color c2 = Vector4.Lerp(light.color, moonLight, Time.deltaTime);
			light.color = c2;
		}else{
			Color c = Vector4.Lerp(skybox.GetColor("_Tint"), day, Time.deltaTime);
			skybox.SetColor("_Tint", c);
			Color c2 = Vector4.Lerp(light.color, sunLight, Time.deltaTime);
			light.color = c2;
		}
	}


	void OnTriggerEnter(Collider other){
		if(other.name.Contains("Player")){
			Tonight = false;
			//skybox.SetColor("_Tint", day);
			//light.color = sunLight;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.name.Contains("Player")){
			Tonight = true;
			//skybox.SetColor("_Tint", night);
			//light.color = moonLight;
		}
	}
}
