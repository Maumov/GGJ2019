using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerControlPanel : MonoBehaviour
{
    [Header("Settings")]
    public HammerController hammer;
    public float interactDistance;
    public float activationDelay;

    private float delay = 0;
    private Renderer material;
    private Transform player;

    private void Start()
    {
        material = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }

        if(Vector3.Distance(player.position, transform.position) <= interactDistance)
        {
            if (delay <= 0 && Input.GetAxisRaw("Fire2") == 1)
            {
                

                if (hammer.GetHammerStatus() == true)
                {
                    TurnedOff();
                }
                else
                {
                    TurnedOn();
                    delay = activationDelay;
                }
            }
        }
    }

    public void TurnedOn()
    {
        hammer.SetHammerStatus(true);
        material.material.mainTextureScale = new Vector2(1f, 1f);
    }

    public void TurnedOff()
    {
        hammer.SetHammerStatus(false);
        material.material.mainTextureScale = new Vector2(1.1f, 1f);
    }

    // Testing view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
}