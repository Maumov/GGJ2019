using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Transform door;
    [Header("Reference Points")]
    public Transform up;
    public Transform down;

    [Header("Movement Variables")]
    public float speed;
    public float restoreSpeed;
    public float distance;
    
    [Header("Button Variables")]
    public Transform ButtonPosition;
    [Range(0f, 3f)]
    public float buttonYdistance;
    public float buttonSpeed;
    //00FF60
    [Header("Lights")]
    public GameObject pendingLight;
    public GameObject completedLight;

    private float exitDamping = 0;
    private float damping = 0;
    private bool openingDoor = false;
    private bool completed = false;
    private Vector3 currentPosition;
    private Vector3 upDefault;
    private Vector3 dowDefault;

    void Start()
    {
        currentPosition = ButtonPosition.position;
        upDefault = up.position;
        dowDefault = down.position; 
    }

    void Update()
    {
		if (openingDoor && !completed)
		{
            door.position = Vector3.Lerp(door.position, dowDefault + Vector3.down, (speed / 10) * Time.deltaTime);
			if (Vector3.Distance(door.position, dowDefault) < distance)
			{
				completed = true;
				door.position = dowDefault + Vector3.down;
                pendingLight.SetActive(false);
                completedLight.SetActive(true);
                //display sound here.
            }
		}

		if (!openingDoor && !completed)
		{
            damping += Time.deltaTime;
			door.position = Vector3.Lerp(door.position, upDefault, restoreSpeed * damping);
		}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            StopAllCoroutines();
            openingDoor = true;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -buttonYdistance, transform.position.z), (speed / 10) * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            damping = 0;
            openingDoor = false;
            StartCoroutine(restorePosition());
        }
    }

    IEnumerator restorePosition()
    {
        while (transform.position != currentPosition)
        {
            exitDamping += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, currentPosition, (buttonSpeed / 2) * exitDamping);
            yield return null;
        }
        exitDamping = 0;
        transform.position = currentPosition;
        yield break;
    }
}