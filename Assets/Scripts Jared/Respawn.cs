using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {

    public GameObject respawnPoint;
    public GameObject objectToRespawn;

    public string myVariable;
    public string deadlyTag;

    public float respawnTime;

    /// <summary>
    /// MAKE A BOOLEAN FOR PLAYER OR NOT
    /// </summary>

    private void Awake()
    {
        objectToRespawn.transform.position = respawnPoint.transform.position;
    }

    IEnumerator RespawnWait(Collision2D other)
    {
        yield return new WaitForSeconds(respawnTime);

        if(other.gameObject.CompareTag(deadlyTag)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            objectToRespawn.transform.position = respawnPoint.transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        StartCoroutine(RespawnWait(other));
    }
}