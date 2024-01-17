using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLoadScreen : MonoBehaviour
{
    private Animator loadingScreenAnimator;
    private AudioManager audioManager;

    private void Start() {
        loadingScreenAnimator = GameObject.Find("LoadScreen").GetComponent<Animator>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void MoveLoadingScreen() {
        loadingScreenAnimator.SetTrigger("MoveClouds");
    }

    public void PlayPlaneCrashingSFX() {
        audioManager.Stop("MenuTheme");
        audioManager.Stop("PlanePropeller");
        audioManager.Play("PlaneBreakDown");
    }
}
