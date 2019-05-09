using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    public int fullyBatteryRate;
    public int mediumBatteryRate;
    public int lowBatteryRate;
    public Renderer render;
    float battery = 100f;
    private float batteryLevel;
    private float scaleY;

    private void Start()
    {
        Energy.onEnergyChange += UpdateBatteryLevel;
        scaleY = transform.localScale.y;
        render.material.EnableKeyword("_EMISSION");
    }

    public void UpdateBatteryLevel()
    {
        batteryLevel = Energy.currentEnergy;

        if (batteryLevel >= fullyBatteryRate)
        {
            render.material.color = Color.green;
        }
        else if(batteryLevel < fullyBatteryRate && batteryLevel >= mediumBatteryRate)
        {
            render.material.color = Color.yellow;
        }
        else if (batteryLevel < mediumBatteryRate && batteryLevel >= lowBatteryRate)
        {
            render.material.color = Color.yellow * Color.red;
            render.material.color = new Color(230, 126, 34, 255);
        }
        else if (batteryLevel < lowBatteryRate && batteryLevel >= 0f)
        {
            render.material.color = Color.red;
        }
        render.material.SetColor("_EmissionColor", render.material.color);

        transform.localScale = new Vector3(transform.localScale.x, scaleY * (batteryLevel / 100f), transform.localScale.z);
    }
}
