using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour {

    public float maxEnergy = 100;
	public float currentEnergy;
    public float recoveryRate;
    public Light lamp;
    public bool recoveryEnergy = false;

    void Start () {
		currentEnergy = maxEnergy;
	}

	void Update () {

        if (!recoveryEnergy)
        {
            if (lamp.isActiveAndEnabled)
                currentEnergy -= Time.deltaTime * 2;
            else
                currentEnergy -= Time.deltaTime;

            if (currentEnergy <= 0f)
            {
                GameOver();
            }
        }
        else
        {
            currentEnergy += recoveryRate / 100;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        }
	}

	public void GameOver(){
        Debug.Log("hi, hora de morir");
	}
}