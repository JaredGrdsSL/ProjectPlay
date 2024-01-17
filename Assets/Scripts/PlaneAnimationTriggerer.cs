using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneAnimationTriggerer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            InfoAndData.numberOfPlays = 3;
            if (Random.Range(1,100) == 1) {
                GameObject e = GameObject.Find("RealisticPlaneCrashing");
                e.GetComponent<Image>().color = Color.white;
                e.GetComponent<Animator>().SetTrigger("Crash");
            }
            else { 
            GameObject.Find("SkyMountains").GetComponent<Animator>().SetTrigger("CrashPlane");
            }
        }
    }
}
