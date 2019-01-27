using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Transform referencePoint;
    public Transform doorUp;
    public Transform doorDown;
    public float speed;
    public float restoreSpeed;
    public float distance;
    private float damping = 0;
    private float exitDamping = 0;
    private bool openingDoor;
    private bool completed = false;
    private Vector3 currentPosition;

    private Vector3 up;
    private Vector3 down;

    void Start()
    {
        up = doorUp.position;
        down = doorDown.position;
    }

    void Update()
    {
        DoorOpening(openingDoor, completed);
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.name.Contains("Player"))
        {
            openingDoor = true;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -0.15f, transform.position.z), speed * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            damping = 0;
            openingDoor = false;
            exitDamping = 20;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0f, transform.position.z), speed * exitDamping);
        }
    }

    public void DoorOpening(bool opening, bool completed)
    {
        if (openingDoor && !completed)
        {
            damping += Time.deltaTime;
            referencePoint.position = Vector3.Lerp(referencePoint.position, down + Vector3.down, (speed / 1000) * damping);
            if (Vector3.Distance(referencePoint.position, down) < distance)
            {
                completed = true;
                referencePoint.position = down + Vector3.down;
                //display sound here.
            }
        }

        if (!openingDoor && !completed)
        {
            referencePoint.position = Vector3.Lerp(referencePoint.position, up, restoreSpeed * Time.deltaTime);
        }
    }
}