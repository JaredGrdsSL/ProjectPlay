using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {

    public GameObject respawnPoint;
    public GameObject objectToRespawn;

    public float respawnTime;

    IEnumerator RespawnWait()
    {
        yield return new WaitForSeconds(respawnTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        objectToRespawn.transform.position = respawnPoint.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        StartCoroutine(RespawnWait());
    }
}
