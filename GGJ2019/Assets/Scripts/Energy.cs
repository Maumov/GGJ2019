using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour {
	public float maxEnergy;
	public float currentEnergy;
	// Use this for initialization
	void Start () {
		currentEnergy = maxEnergy;
	}
	
	// Update is called once per frame
	void Update () {
		currentEnergy -= Time.deltaTime;
		if(currentEnergy < 0f){
			GameOver();	
		}

	}

	public void GameOver(){
		
	}

	public void BackToMainMenu(){
		
	}
}
