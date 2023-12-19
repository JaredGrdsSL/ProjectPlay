using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAnimationTriggerer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            GameObject.Find("SkyMountains").GetComponent<Animator>().SetTrigger("CrashPlane");
        }
    }
}
