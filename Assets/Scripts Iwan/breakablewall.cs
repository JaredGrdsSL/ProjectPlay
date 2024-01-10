using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public GameObject particles;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioManager.Play("breakingwall");
            Destroy(Instantiate(particles, new Vector3(transform.position.x, transform.position.y + 1.75f, transform.position.z), Quaternion.Euler(0,0,0)), 5);
            Destroy(gameObject);
        }
    }
}