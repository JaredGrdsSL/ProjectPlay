using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Respawn : MonoBehaviour {

    public GameObject respawnPoint;
    public GameObject objectToRespawn;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")) {
            objectToRespawn.transform.position = respawnPoint.transform.position;
        }
    }
}