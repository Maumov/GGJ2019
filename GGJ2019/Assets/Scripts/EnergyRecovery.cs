using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRecovery : MonoBehaviour
{
    public Energy energy;
    public GameObject[] particles;


    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Player"))
        {
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
            Debug.Log("lel");
            foreach (GameObject particle in particles)
            {
                particle.SetActive(false);
            }
        }
    }
}