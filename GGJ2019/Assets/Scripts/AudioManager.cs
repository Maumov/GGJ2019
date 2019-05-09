using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource danger;
    public AudioSource safe;
    public float switchSpeed;
    bool inside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            inside = true;
            
            StopAllCoroutines();
            StartCoroutine(IncreaseValue());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            inside = false;
           

            StopAllCoroutines();
            StartCoroutine(IncreaseValue());
        }
    }

    IEnumerator IncreaseValue()
    {
        if (inside)
        {
            while(safe.volume < 1)
            {
                safe.volume += Time.deltaTime * switchSpeed;
                danger.volume -= Time.deltaTime * switchSpeed;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (danger.volume < 1)
            {
                danger.volume += Time.deltaTime * switchSpeed;
                safe.volume -= Time.deltaTime * switchSpeed;
                yield return new WaitForEndOfFrame();
            }
        }
        yield return null;
    }
}