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
	public GameObject panel;
	public Text objectives;
    private int point = 0;
	 
	void Start(){
		panel.SetActive(true);
		objectives.text = point + "/" + "5" + "Objetos por recoger";
		Invoke("ApagarTexto", 3f);
	}

    void Update()
    {
        if (objectAvailable)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                point++;
                player.GetComponent<Inventory>().currentItem = null;
				Destroy(player.GetComponent<Inventory>().referencePosition.GetChild(0).gameObject);
				panel.SetActive(true);
				objectives.text = point + "/" + "5" + "Objetos por recoger";
				Invoke("ApagarTexto", 3f);
            }
			if(point == 5){
				Win();
			}
        }
    }

	void Win(){
		
	}

	void ApagarTexto(){
		panel.SetActive(false);
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