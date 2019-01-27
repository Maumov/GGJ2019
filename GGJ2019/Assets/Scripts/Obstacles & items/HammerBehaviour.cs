using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBehaviour : MonoBehaviour
{
    public Transform player;
    public float fallSpeed;
    public float riseSpeed;
    public float angularSpeed;
    public float fallingDelay;
    public float riseDelay;

    private Quaternion defaultRotation;
    private Quaternion yRotation;
    private float rotationIncrease = 0;
    private Vector3 dir;
    private bool falling = false;
    private bool smashing = false;
    private bool rotationComplete = false;
    private bool lookAt = false;
    Quaternion lookAtPlayer;

    void Start()
    {
        defaultRotation = Quaternion.identity;
    }

    void Update()
    {
        if (!rotationComplete)
        {
            if (!lookAt)
            {
                dir = (player.position - transform.position);
                dir.y = transform.position.y;
                lookAtPlayer = Quaternion.LookRotation(dir);
                lookAt = true;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtPlayer, angularSpeed * Time.deltaTime);

            if (transform.rotation == lookAtPlayer)
            {
                lookAt = false;
                rotationComplete = true;
                defaultRotation = transform.rotation;
            }
        }

        if (rotationComplete)
        {
            if (transform.rotation == defaultRotation)
            {
                lookAtPlayer = Quaternion.LookRotation(Vector3.down, dir);
                falling = true;
            }

            if (falling)
            {
                StartCoroutine(rotations());
                if (smashing)
                {
                    StopAllCoroutines();
                    rotationIncrease += Time.deltaTime;
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookAtPlayer, fallSpeed * rotationIncrease);
                }

                if (transform.rotation == lookAtPlayer)
                {
                    smashing = false;
                    falling = false;
                }
            }

            if (!falling)
            {
                StartCoroutine(rotations());
                if (smashing)
                {
                    StopAllCoroutines();
                    transform.rotation = Quaternion.Lerp(transform.rotation, defaultRotation, riseSpeed * Time.deltaTime);
                }

                if (transform.rotation == defaultRotation)
                {
                    rotationIncrease = 0;
                    smashing = false;
                    falling = true;
                    rotationComplete = false;
                }
            }
        }
    }

    IEnumerator rotations()
    {
        if (falling)
        {
            yield return new WaitForSeconds(fallingDelay);
            smashing = true;
        }
        else
        {
            yield return new WaitForSeconds(riseDelay);
            smashing = true;
        }
    }
}