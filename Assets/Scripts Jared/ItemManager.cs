using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    private EnergyManager energyManager;
    private AudioManager audioManager;
    public new GameObject particleSystem;

    private void Start()
    {
        energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) {
            PlayerPrefs.SetInt("energys", PlayerPrefs.GetInt("energys", 0) + 1);
            energyManager.GainEnergy(10f * PlayerPrefs.GetFloat("energyMultiplier", .7f));
            Destroy(Instantiate(particleSystem, gameObject.transform.position, gameObject.transform.rotation), 3);
            audioManager.Play("EnergyPickup");
            Destroy(gameObject);
        }
    }
}
