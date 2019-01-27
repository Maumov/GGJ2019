using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehaviour : MonoBehaviour
{
    public float fallSpeed;
    public float riseSpeed;
    public float verticalPosition;
    public float distance;
    public float fallingDelay;
    public float riseDelay;

    public float damping = 0;
    private bool restart = true;
    private bool temp = false;
    private bool falling = false;
    private Vector3 SmashPosition;
    private Vector3 defaultPosition;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, verticalPosition, transform.position.z);
        defaultPosition = transform.position;
        SmashPosition = transform.position;
        SmashPosition.y = 1;
    }

    void Update()
    {
        if(restart)
        {
            falling = true;
            restart = false;
        }

        if (falling)
        {
            StartCoroutine(SmashBehaviour());
            if (temp)
            {
                StopAllCoroutines();
                damping += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, SmashPosition, fallSpeed * damping);
            }

            if (Vector3.Distance(transform.position, SmashPosition) < distance)
            {
                falling = false;
                temp = false;
            }
        }

        if(falling == false)
        {
            StartCoroutine(SmashBehaviour());
            if (temp)
            {
                StopAllCoroutines();
                transform.position = Vector3.Lerp(transform.position, defaultPosition, riseSpeed * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, defaultPosition) < distance)
            {
                temp = false;
                restart = true;
                damping = 0;
            }
        }
    }

    IEnumerator SmashBehaviour()
    {
        if (falling)
        {
            yield return new WaitForSeconds(fallingDelay);
            temp = true;
        }
        else
        {
            yield return new WaitForSeconds(riseDelay);
            temp = true;
        }
    }
}