using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {

    public GameObject respawnPoint;
    public GameObject objectToRespawn;
    public string deadlyTag;

    public float respawnTime;

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
