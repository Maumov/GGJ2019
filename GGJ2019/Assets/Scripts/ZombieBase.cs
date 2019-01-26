using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieStatus{idle, chasing}

public class ZombieBase : MonoBehaviour {

	NavMeshAgent agent;
	public ZombieStatus zombieStatus = ZombieStatus.idle;
	PlayerMovement player;
	public float range;
	Vector3 startPosition;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator>();
		agent = GetComponent<NavMeshAgent>();
		player = FindObjectOfType<PlayerMovement>();
		startPosition = transform.position;
		StartCoroutine(CheckRange());
		StartCoroutine(Idleing());
	}
	
	// Update is called once per frame
	void Update () {
		float v = agent.velocity.magnitude;
		v = Mathf.Clamp(v, 0f , 1f);
		anim.SetFloat("Move", v);
	}

	void StartChasing(){
		agent.SetDestination(player.transform.position);
	}
	void StopChasing(){
		agent.SetDestination(startPosition);
	}

	IEnumerator Idleing(){
		while(zombieStatus == ZombieStatus.idle){
			agent.SetDestination(startPosition + new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)));	
			yield return new WaitForSeconds(Random.Range(3f, 10f));
		}

	}

	IEnumerator Chasing(){
		while(zombieStatus == ZombieStatus.chasing){
			agent.SetDestination(player.transform.position);	
			yield return new WaitForSeconds(1f);
		}
	}


	IEnumerator CheckRange(){
		while(true){
			if(player != null){
				if(zombieStatus == ZombieStatus.idle){
					if(Vector3.Distance(player.transform.position, transform.position) < range){
						zombieStatus = ZombieStatus.chasing;

						StartCoroutine(Chasing());
					}	
				}
				if(zombieStatus == ZombieStatus.chasing){
					if(Vector3.Distance(player.transform.position, transform.position) > range){
						zombieStatus = ZombieStatus.idle;
						StartCoroutine(Idleing());
					}	
				}

			}else{
				player = FindObjectOfType<PlayerMovement>();
			}

			yield return new WaitForSeconds(2f);
		}
	}
}
