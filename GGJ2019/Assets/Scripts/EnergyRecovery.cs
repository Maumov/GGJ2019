using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRecovery : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 50;
    public float recoveryRate;
    private bool updateHealth = false;

    void Update()
    {
        if (updateHealth)
        {
            health += recoveryRate / 100;
            health = Mathf.Clamp(health, 0, maxHealth);
            Debug.Log("Health: " + health);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            updateHealth = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            updateHealth = false;
        }
    }
}