using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour {
    public Image energyBar;
    private Light2D playerLight;
    private PlayerMovement playerMovement;
    private CaveEnterTrigger caveEnterTrigger;


    [Range(1f, 100f)]
    public int depletionTime;

    private float energyAmount = 100f;

    private void Start()
    {
        playerLight = GameObject.Find("PlayerLight").GetComponent<Light2D>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        caveEnterTrigger = GameObject.Find("CaveLightTrigger").GetComponent<CaveEnterTrigger>();

    }

    void Update()
    {
        if(playerMovement.isBat) {
            float depletionAmount = 100f / depletionTime * Time.deltaTime;
            energyAmount -= depletionAmount;

            if (caveEnterTrigger.InCave) { 
                float toBeValue = 1f / 100 * energyAmount;
                playerLight.intensity = Mathf.Lerp(playerLight.intensity, toBeValue, 1 * Time.deltaTime);
            }

        }

        energyBar.fillAmount = energyAmount / 100f;
        energyAmount = Mathf.Clamp(energyAmount, 0, 100);

    }

    public void TakeEnergy(float amount)
    {
        energyAmount -= amount;
        energyBar.fillAmount = energyAmount / 100f;
    }

    public void GainEnergy(float amount)
    {
        energyAmount += amount;
        energyBar.fillAmount = energyAmount / 100f;
    }
}
