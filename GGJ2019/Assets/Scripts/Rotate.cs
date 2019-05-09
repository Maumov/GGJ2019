using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Range(0f, 1f)]
    public float movementSpeed;
    public float speed = 100;
    public bool isItem = false;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
        if (isItem)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time) * Time.deltaTime * movementSpeed, transform.position.z);
        }
    }
}