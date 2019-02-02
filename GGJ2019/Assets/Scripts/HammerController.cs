using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    [Header("Switches")]
    public bool start = true;

    [Header("Base")]
    public Transform player;
    public Transform rotativeM;
    public Transform basePosition;
    public float baseRotarion;
    public float seekAndDestroy;

    [Header("Body")]
    public Transform body;
    public float fallingSpeed;
    public float raisingSpeed;
    public float fallDelay;
    public float raiseDelay;
    public float damping;

    private Rigidbody rotativeRigidbody;
    private Vector3 vectorDirection;
    private Quaternion playerRotation;
    private Quaternion previousRotation;
    private Quaternion verticalRotation;

    void Start()
    {
        rotativeRigidbody = rotativeM.GetComponent<Rigidbody>();
        StartCoroutine(Behaviour());
    }

    IEnumerator Behaviour()
    {
        while (start)
        {
            damping = 0;
            vectorDirection = player.position - rotativeM.position;
            vectorDirection.y = basePosition.localPosition.y;
            playerRotation = Quaternion.LookRotation(vectorDirection);

            while (Quaternion.Angle(rotativeRigidbody.rotation, playerRotation) > 1)
            {
                rotativeRigidbody.rotation = Quaternion.Lerp(rotativeRigidbody.rotation, playerRotation, baseRotarion * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            rotativeRigidbody.rotation = playerRotation;
            yield return new WaitForSeconds(fallDelay);

            previousRotation = body.rotation;
            verticalRotation = Quaternion.LookRotation(-body.up, body.forward);

            while (Quaternion.Angle(body.rotation, verticalRotation) > 1)
            {
                damping += Time.deltaTime;
                body.rotation = Quaternion.Lerp(body.rotation, verticalRotation, fallingSpeed * damping);
                yield return new WaitForEndOfFrame();
            }

            body.rotation = verticalRotation;
            yield return new WaitForSeconds(raiseDelay);

            while (Quaternion.Angle(body.rotation, previousRotation) > 1)
            {
                body.rotation = Quaternion.Lerp(body.rotation, previousRotation, raisingSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            body.rotation = previousRotation;
            yield return new WaitForSeconds(seekAndDestroy);
        }
    }
}