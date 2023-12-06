using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public GameObject particles;

    void Start()
    {
        // Initialization code can go here if needed
    }

    void Update()
    {
        // Update code can go here if needed
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destroy the wall
            Destroy(gameObject);

            // Instantiate particles at the wall's position
            GameObject instantiatedParticles = Instantiate(particles, transform.position, transform.rotation);
        }
    }
}