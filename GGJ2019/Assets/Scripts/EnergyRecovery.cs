using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRecovery : MonoBehaviour
{
    public Energy energy;
    public GameObject[] particles;
    public AudioSource charging;
    public AudioSource notcharging;
    [Range(0f, 4f)]
    public float soundDelay;
    bool notCharging = true;
    bool activeSound;

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            if (notCharging && activeSound)
            {
                charging.Play();
                notCharging = false;
                activeSound = false;
                StartCoroutine(soundTrigger());
            }
            energy.recoveryEnergy = true;
            foreach(GameObject particle in particles) {
                particle.SetActive(true);
            }
            particles[3].transform.position = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            energy.recoveryEnergy = false;
            foreach (GameObject particle in particles)
            {
                particle.SetActive(false);
            }
            if (notCharging && activeSound)
            {
                notcharging.Play();
                notCharging = true;
                activeSound = false;
                StartCoroutine(soundTrigger());
            }
        }
    }

    IEnumerator soundTrigger()
    {
        yield return new WaitForSeconds(soundDelay);
        activeSound = true;
        yield break;
    }
}