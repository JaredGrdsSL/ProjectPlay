using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour {
    public GameObject energyshown;
    public Image energyBar;
    private GameObject playerLight;
    private PlayerMovement playerMovement;


    [Range(1f, 100f)]
    public int depletionTime;

    private float energyAmount = 100f;

    private void Start()
    {
        energyshown.SetActive(false);
        playerLight = GameObject.Find("PlayerLight");
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

    }

    void Update()
    {
        if(playerMovement.isBat) {
            float depletionAmount = 100f / depletionTime * Time.deltaTime;
            energyAmount -= depletionAmount;
            showenergy();
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
    void showenergy()
    {
        energyshown.SetActive(true);
    }
}
