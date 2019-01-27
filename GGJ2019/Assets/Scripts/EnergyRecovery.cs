using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRecovery : MonoBehaviour
{
    public Energy energy;

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            energy.recoveryEnergy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            energy.recoveryEnergy = false;
        }
    }
}