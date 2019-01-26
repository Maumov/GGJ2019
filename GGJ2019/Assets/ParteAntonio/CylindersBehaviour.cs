using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylindersBehaviour : MonoBehaviour
{
    public float rotationSpeed;
    public Transform[] cylinder;

    void Update()
    {
        cylinder[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        cylinder[1].Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
    }
}
