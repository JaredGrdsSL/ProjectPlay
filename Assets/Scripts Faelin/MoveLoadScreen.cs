using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLoadScreen : MonoBehaviour
{
    private Animator loadingScreenAnimator;

    private void Start() {
        loadingScreenAnimator = GameObject.Find("LoadScreen").GetComponent<Animator>();
    }

    public void MoveLoadingScreen() {
        loadingScreenAnimator.SetTrigger("MoveClouds");
    }
}
