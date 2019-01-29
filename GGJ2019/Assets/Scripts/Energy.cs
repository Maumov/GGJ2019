using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Energy : MonoBehaviour {

    public float maxEnergy = 100;
	public float currentEnergy;

	bool usingLight;
	public Light theLight;
	public BipedIK biped;


    public float recoveryRate;
    public Light lamp;
    public bool recoveryEnergy = false;

	public Slider slider;
	public GameObject deadParticle;
	bool isDead;
	public GameObject GameOverImage;
	public AudioSource audioGameOver;
	// Use this for initialization
    void Start () {
		currentEnergy = maxEnergy;
	}

	void Update () {
		currentEnergy -= Time.deltaTime * (usingLight? 2f : 1f);

		if(currentEnergy <= 0f){
			GameOver();	
		}
		if(Input.GetButtonDown("Fire1")){
			usingLight = !usingLight;
			theLight.enabled = usingLight;
			biped.solvers.rightHand.IKPositionWeight = usingLight? 1f : 0f ;
		}
		if(recoveryEnergy){
			currentEnergy += recoveryRate * Time.deltaTime;
		}
		currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
		slider.value = currentEnergy/maxEnergy;
	}

	public void GameOver(){
		//GetComponentInChildren<Animator>().SetTrigger("Death");
		Invoke("BackToMainMenu",2.5f);
		if(!isDead){
			Instantiate(deadParticle,transform.position, Quaternion.identity);	
		}
		audioGameOver.Play();
		isDead = true;
	}

	public void BackToMainMenu(){
		GameOverImage.SetActive(true);
		Invoke("BackToMainMenu2",2.5f);
	}
	public void BackToMainMenu2(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void BackToMainMenu3(){
		Invoke("BackToMainMenu2",2.5f);
	}
}