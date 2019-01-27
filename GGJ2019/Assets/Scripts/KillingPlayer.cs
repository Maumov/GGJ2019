using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingPlayer : MonoBehaviour
{
    private Energy energy;
    
    void Start()
    {
        energy = GameObject.FindGameObjectWithTag("Player").GetComponent<Energy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            energy.GameOver();
        }
    }

}
