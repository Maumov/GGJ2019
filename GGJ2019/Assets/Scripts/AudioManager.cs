using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource danger;
    public AudioSource safe;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            danger.Stop();
            safe.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            safe.Stop();
            danger.Play();
        }
    }
}
