using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysOnScript : MonoBehaviour {
    public static bool haveGottenIt;

    private void Start() {
        if (haveGottenIt) {
            Debug.Log("GottenEnergy");
            InfoAndData.energys = PlayerPrefs.GetInt("energys", 0);
            haveGottenIt = false;
        }
        PlayerPrefs.SetInt("energys", InfoAndData.energys);
    }
}
