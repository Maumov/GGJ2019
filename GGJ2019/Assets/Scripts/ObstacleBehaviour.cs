using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [Header("Obstacle")]
    public float delayConst = 2;
    public float fallSpeed = 0.2f;
    public float rising = 5;
    public float horizontalSpeed = 5;
    public float verticalPosition = 20;
    public float smashLateralSpeed = 5;
    public GameObject obstacle;
    public Vector3 firstPosition;

    [Header("Smash")]
    public float delayBetweenSmash = 2;
    public float groundedDelay = 1;
    public float dampingDelay = 1;
    public float vibrate = 1;

    private bool seekAndDestroy = true;
    private bool seekPlayer = false;
    private bool grounded = false;
    private bool goUp = false;
    private bool goDown = true;
    private bool falling = true;
    private bool shake = false;
    private bool chase = false;
    private bool readyToSmash = false;

    [Header("ETC")]
    public Transform player;
    public float distance = 0.1f;
    private float damping = 0;

    private Transform[] positions;
    private Vector3 currentPlayerPosition;
    private Vector3 SmashPosition;
    private Vector3 tempPosition;

    void Start()
    {
        obstacle.transform.position = new Vector3(firstPosition.x, verticalPosition, firstPosition.z);

        currentPlayerPosition = new Vector3(player.transform.position.x, verticalPosition, player.transform.position.z);
    }

    private void Update()
    {
        if (goDown && !readyToSmash && (Vector3.Distance(obstacle.transform.position, currentPlayerPosition) < distance))
        {
            StopAllCoroutines();
            
            readyToSmash = true;
            falling = true;
            seekPlayer = false;
            goDown = false;
            shake = true;
            SmashPosition = new Vector3(currentPlayerPosition.x, player.transform.position.y + 0.5f, currentPlayerPosition.z);
        }

        if(chase && goDown && seekAndDestroy && !readyToSmash && !(Vector3.Distance(obstacle.transform.position, currentPlayerPosition) < distance))
        {
            currentPlayerPosition = new Vector3(player.transform.position.x, verticalPosition, player.transform.position.z);
            seekAndDestroy = false;
            seekPlayer = true;
        }

        if (seekPlayer)
        {
            obstacle.transform.position = Vector3.Lerp(obstacle.transform.position, currentPlayerPosition, horizontalSpeed * Time.deltaTime);
        }

        if (readyToSmash)
        {
            if (falling && !(Vector3.Distance(obstacle.transform.position, SmashPosition) < distance))
            {
                damping += Time.deltaTime;
                obstacle.transform.position = Vector3.Lerp(obstacle.transform.position, SmashPosition, fallSpeed * damping);
            }
            else
            {
                if (shake)
                {
                    obstacle.transform.position = Vector3.Lerp(SmashPosition, SmashPosition + new Vector3(Random.Range(-vibrate, vibrate), 0f, 0f), Time.deltaTime);
                }
                grounded = true;
                falling = false;
            }

            if (grounded && !(Vector3.Distance(obstacle.transform.position, currentPlayerPosition) < distance))
            {
                StartCoroutine(SmashBehaviour());
                if (goUp)
                {
                    StopAllCoroutines();
                    obstacle.transform.position = Vector3.Lerp(obstacle.transform.position, currentPlayerPosition, rising * Time.deltaTime);
                }
            }

            if (grounded && (Vector3.Distance(obstacle.transform.position, currentPlayerPosition) < distance))
            {
                grounded = false;
                seekAndDestroy = true;
                seekPlayer = false;
                readyToSmash = false;
                goUp = false;
                damping = 0;
                StartCoroutine(SmashBehaviour());
            }
        }
    }

    IEnumerator SmashBehaviour()
    {
        if (readyToSmash)
        {
            yield return new WaitForSeconds(dampingDelay);
            obstacle.transform.position = SmashPosition;
            shake = false;
            yield return new WaitForSeconds(groundedDelay);
            goUp = true;
        }
        else
        {
            yield return new WaitForSeconds(delayBetweenSmash);
            currentPlayerPosition = new Vector3(player.transform.position.x, verticalPosition, player.transform.position.z);
            goDown = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            chase = true;
            Debug.Log(chase);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            chase = false;
        }
    }
}