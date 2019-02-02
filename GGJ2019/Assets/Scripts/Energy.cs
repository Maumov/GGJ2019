using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Energy : MonoBehaviour {

    public PlayerMovement speed;
    public GameObject body;
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
    bool isDead = false;
    bool outside = false;
	public GameObject GameOverImage;
    public AudioSource audioGameOver;
	// Use this for initialization
    void Start () {
		currentEnergy = maxEnergy;
	}

	void Update () {
        if (outside)
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
		if(!isDead){
            Invoke("BackToMainMenu", 2.5f);
            Instantiate(deadParticle,transform.position, Quaternion.identity);
            audioGameOver.Play();
            speed.MovementSpeed = 0;
            body.SetActive(false);
            isDead = true;
        }
	}

	public void BackToMainMenu(){
		GameOverImage.SetActive(true);
		Invoke("BackToMainMenu2", 3f);
	}
	public void BackToMainMenu2(){
        isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void BackToMainMenu3(){
        Invoke("BackToMainMenu2",2.5f);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Home"))
        {
            outside = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Home"))
        {
            outside = true;
        }
    }
}