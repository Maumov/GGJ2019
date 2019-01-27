using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiverController : MonoBehaviour
{
    
    public GameObject player;
    private bool objectAvailable;
    public GameObject canvasLocal;
    public GameObject canvasGlobal;
    public GameObject item;
    public Transform[] position;
    private int point = 0;

    void Update()
    {
        if (objectAvailable)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                Debug.Log(point.ToString() + item.transform.position);
                item.transform.SetParent(position[point].transform);
                item.transform.position = position[point].transform.position;
                item.transform.localScale = Vector3.one * 0.7f;
                item.transform.rotation = Quaternion.LookRotation(Vector3.up);
                point++;
                player.GetComponent<Inventory>().currentItem = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            canvasLocal.SetActive(true);
            canvasGlobal.SetActive(true);
            if (item = player.GetComponent<Inventory>().currentItem)
            {
                objectAvailable = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Player"))
        {
            canvasLocal.SetActive(false);
            canvasGlobal.SetActive(false);
            if (player.GetComponent<Inventory>().currentItem)
            {
                objectAvailable = false;
            }
        }
    }
}