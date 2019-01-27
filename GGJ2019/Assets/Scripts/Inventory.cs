using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    public GameObject currentItem;
    public Transform referencePosition;
    public float angularSpeed = 100;
    public Transform position;

    void Update()
    {
        if (currentItem != null)
        {
            currentItem.transform.Rotate(Vector3.up * angularSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
		if (other.tag.Equals("InGameObject"))
        {
            if (currentItem == null)
            {
                currentItem = other.gameObject;
                Collider collider = currentItem.GetComponent<Collider>();
                collider.enabled = false;
                currentItem.transform.position = referencePosition.position;
				currentItem.transform.SetParent(referencePosition);
            }
            else
                Debug.Log("No more items available.");
        }
    }

    public void DeactivateChildObject()
    {
        currentItem.transform.parent = null;
        currentItem = null;
    }
}