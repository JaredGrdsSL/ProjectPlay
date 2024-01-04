using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour {
    public Image energyBar;
    [Range(1f, 100f)]
    public int depletionTime;

    private float energyAmount = 100f;

    void Update()
    {
        float depletionAmount = 100f / depletionTime * Time.deltaTime;
        energyAmount -= depletionAmount;

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
