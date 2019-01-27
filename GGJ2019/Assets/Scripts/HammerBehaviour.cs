using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBehaviour : MonoBehaviour
{
    public Transform player;
    public float fallSpeed;
    public float riseSpeed;
    public float angularSpeed;
    public float fallDelay;
    public float riseDelay;

    public float damping = 0;
    private bool activate = true;
    private bool falling = true;
    private bool rotationCompleted = false;
    private bool looking = false;

    private bool rotate;
    private bool fall;
    private bool rise;

    private Vector3 direction;
    private Quaternion previousRotation;
    private Quaternion currentRotation;
    private Quaternion lookAt;

    void Start()
    {
        StartCoroutine(rotations());
    }

    IEnumerator rotations()
    {
        while (activate)
        {
            if (!rotationCompleted)
            {
                if (!looking)
                {
                    direction = (player.position - transform.position);
                    direction.y = transform.position.y;
                    lookAt = Quaternion.LookRotation(direction);
                    looking = true;
                }

                transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, angularSpeed * Time.deltaTime);

                if (transform.rotation == lookAt)
                {
                    rotationCompleted = true;
                    currentRotation = transform.rotation;
                    previousRotation = currentRotation;
                }
            }

            if (rotationCompleted)
            {
                if (transform.rotation == currentRotation)
                {
                    lookAt = Quaternion.LookRotation(direction, Vector3.up);
                    falling = true;
                }

                looking = false;

                if (falling)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, fallSpeed * Time.deltaTime);

                    if (transform.rotation == lookAt)
                    {
                        falling = false;
                        yield return new WaitForSeconds(riseDelay);
                    }
                }

                if (!falling)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, previousRotation, riseSpeed * Time.deltaTime);

                    if (transform.rotation == previousRotation)
                    {
                        falling = true;
                        rotationCompleted = false;
                        looking = false;
                    }
                }
            }
        }
    }
}