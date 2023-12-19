using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public Image energyBar;
    public float energyAmount = 100f;

    void Start()
    {

    }

    void Update()
    {
        Debug.Log(Time.time % 1);
        if(Time.time % 1 == 0) {
            TakeEnergy(1f);
        }
    }

    public void TakeEnergy(float amount)
    {
        energyAmount -= amount;
        energyBar.fillAmount = energyAmount / 100f;
    }

    public void GainEnergy(float amount)
    {
        energyAmount += amount;
        energyAmount = Mathf.Clamp(energyAmount, 0, 100);

        energyBar.fillAmount = energyAmount / 100f;
    }
}
