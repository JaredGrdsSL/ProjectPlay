using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    AudioManager audioManager;
    private void Start() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    public void LoadLevel() {
        audioManager.Play("LevelTheme");
        audioManager.Stop("WindWooshing");
        SceneManager.LoadScene(1);
    }
}
