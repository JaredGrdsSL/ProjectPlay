using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    private Animator planeAnimator;
    private Animator buttonsAnimator;
    private Animator SettingsPannelAnimator;
    private Animator credditsPannelAnimator;
    private Animator shopPannelAnimator;

    private TextMeshProUGUI energyCounter;

    private bool canStartGame = true;

    //settings
    public AudioMixer mainAudioMixer;
    private Slider musicSlider;
    private Slider sfxSlider;

    private void Start() {
        planeAnimator = GameObject.Find("Plane").GetComponent<Animator>();
        buttonsAnimator = GameObject.Find("Buttons").GetComponent<Animator>();
        SettingsPannelAnimator = GameObject.Find("SettingsPannel").GetComponent<Animator>();
        credditsPannelAnimator = GameObject.Find("CredditsPannel").GetComponent<Animator>();
        energyCounter = GameObject.Find("EnergyCounter").GetComponent<TextMeshProUGUI>();
        shopPannelAnimator = GameObject.Find("ShopPannel").GetComponent<Animator>();

        //settings
        //set audio sliders in settings to the value of the audio mixers
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();

        energyCounter.text = InfoAndData.energys.ToString();
        float mixerVolume;
        mainAudioMixer.GetFloat("MusicVolume", out mixerVolume);
        musicSlider.value = mixerVolume;
        mainAudioMixer.GetFloat("SFXVolume", out mixerVolume);
        sfxSlider.value = mixerVolume;
    }

    public void StartGame() {
        if (canStartGame) {
            canStartGame = false;
            HideButtons();
            planeAnimator.SetTrigger("CrashPlane");
        }
    }

    public void ExitGame() {
        if (canStartGame) {
            Application.Quit();
        }
    }

    public void Creddits() {
        if (canStartGame) {
            canStartGame = false;
            HideButtons();
            credditsPannelAnimator.SetBool("CredditsPannelVisable", true);
        }
    }

    public void CredditsBack() {
        canStartGame = true;
        UnHideButtons();
        credditsPannelAnimator.SetBool("CredditsPannelVisable", false);
    }

    public void Settings() {
        if (canStartGame) {
            canStartGame = false;
            HideButtons();
            SettingsPannelAnimator.SetBool("SettingsPannelVisble", true);
        }
    }

    public void SettingsBack() {
        canStartGame = true;
        UnHideButtons();
        SettingsPannelAnimator.SetBool("SettingsPannelVisble", false);
    }

    public void Shop() {
        if (canStartGame) {
            canStartGame = false;
            HideButtons();
            shopPannelAnimator.SetBool("IsDown", true);
        }
    }

    public void ShopBack() {
        canStartGame = true;
        UnHideButtons();
        shopPannelAnimator.SetBool("IsDown", false);
    }

    private void HideButtons() {
        buttonsAnimator.SetBool("Hidden", true);
        shopPannelAnimator.SetBool("IsHidden", true);
    }

    private void UnHideButtons() {
        buttonsAnimator.SetBool("Hidden", false);
        shopPannelAnimator.SetBool("IsHidden", false);
    }

    //settings functions
    public void SetMusic(float value) {
        mainAudioMixer.SetFloat("MusicVolume", value);
    }

    public void SetSfx(float value) {
        mainAudioMixer.SetFloat("SFXVolume", value);
    }

    //creddits
    public void OpenIwan() {
        Application.OpenURL("https://ps230050.github.io/portfolio/");
    }

    public void OpenJared() {
        Application.OpenURL("https://jaredgrdssl.github.io/");
    }
    public void OpenFaelin() {
        Application.OpenURL("https://faelinportfolio.vercel.app/");
    }
    public void OpenRinad() {
        Application.OpenURL("https://www.behance.net/jiyeonmin5");
    }
    public void OpenKarolina() {
        Application.OpenURL("https://www.behance.net/karolinadurlik");
    }
    public void OpenAura() {
        Application.OpenURL("https://www.instagram.com/witte_spin/");
    }
    public void OpenYanick() {
        Application.OpenURL("https://www.instagram.com/redresolution_official/");
    }
}
