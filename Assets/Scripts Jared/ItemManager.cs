using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    private EnergyManager energyManager;
    public new GameObject particleSystem;

    private void Start()
    {
        energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) {
            InfoAndData.energys++;
            energyManager.GainEnergy(10f);
            Destroy(Instantiate(particleSystem, gameObject.transform.position, gameObject.transform.rotation), 3);
            Destroy(gameObject);
        }
    }
}
