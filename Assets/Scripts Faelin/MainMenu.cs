using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartGame() {
        //play crashing animation then trigger LoadLevel()
    }

    public void LoadLevel() {
        SceneManager.LoadScene(1);
    }

    public void Creddits() {

    }

    public void Settings() {
        SceneManager.LoadScene(2);
    }
    public void ExitGame() { 
        Application.Quit();
    }
}
