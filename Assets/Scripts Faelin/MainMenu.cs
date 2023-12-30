using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    private Animator planeAnimator;
    private Animator buttonsAnimator;

    private bool canInteract = true;

    private void Start() {
        planeAnimator = GameObject.Find("Plane").GetComponent<Animator>();
        buttonsAnimator = GameObject.Find("Buttons").GetComponent<Animator>();
    }

    public void StartGame() {
        canInteract = false;
        buttonsAnimator.SetTrigger("Hide");
        planeAnimator.SetTrigger("CrashPlane");
    }

    public void Creddits() {
        if (canInteract) {
            SceneManager.LoadScene(3);
        }
    }

    public void Settings() {
        if (canInteract) {
            SceneManager.LoadScene(2);
        }
    }
    public void ExitGame() {
        if (canInteract) {
            Application.Quit();
        }
    }
}
