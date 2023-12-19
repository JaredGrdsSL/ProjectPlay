using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private EnergyManager energyManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) {
            energyManager.GainEnergy(10f);
            Destroy(gameObject);
        }
    }
}
