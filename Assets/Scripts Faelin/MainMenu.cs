using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    private AudioManager audioManager;

    private TextMeshProUGUI energyCounter;

    private bool canStartGame = true;
    private bool canCrashPlane = false;
    private float countDown = 0;

    //settings
    public AudioMixer mainAudioMixer;
    private Slider musicSlider;
    private Slider sfxSlider;

    //shop
    private Image energyBoosterBar;
    private TextMeshProUGUI energyBoostCostText;
    private Image scoreBoosterBar;
    private TextMeshProUGUI scoreBoostCostText;

    private void Start() {
        //reset player prefs
        //PlayerPrefs.SetInt("energys", 0);
        //PlayerPrefs.SetFloat("energyMultiplier", .7f);
        //PlayerPrefs.SetInt("Highscore", 0);
        //PlayerPrefs.SetFloat("ScoreMultiplier", .7f);
        planeAnimator = GameObject.Find("Plane").GetComponent<Animator>();
        buttonsAnimator = GameObject.Find("Buttons").GetComponent<Animator>();
        SettingsPannelAnimator = GameObject.Find("SettingsPannel").GetComponent<Animator>();
        credditsPannelAnimator = GameObject.Find("CredditsPannel").GetComponent<Animator>();
        energyCounter = GameObject.Find("EnergyCounter").GetComponent<TextMeshProUGUI>();
        shopPannelAnimator = GameObject.Find("ShopPannel").GetComponent<Animator>();
        energyBoosterBar = GameObject.Find("EnergyBoostUpgradeBarFiller").GetComponent<Image>();
        energyBoostCostText = GameObject.Find("EnergyBoostCostText").GetComponent<TextMeshProUGUI>();
        scoreBoosterBar = GameObject.Find("ScoreBoostUpgradeBarFiller").GetComponent<Image>();
        scoreBoostCostText = GameObject.Find("ScoreBoostCostText").GetComponent<TextMeshProUGUI>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        audioManager.Play("WindWooshing");
        audioManager.Play("PlanePropeller");
        audioManager.Play("MenuTheme");

        //settings
        //set audio sliders in settings to the value of the audio mixers
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();

        mainAudioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolumeSaved", 0));
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolumeSaved", 0);
        mainAudioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolumeSaved", 0));
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolumeSaved", 0);

        SetEnergyCounterApearance();
        SetEnergyBarBoostFillApearance();
        SetScoreBarBoostFillApearance();
    }

    private void Update() {
        if (!canCrashPlane) {
            countDown += Time.deltaTime;
        }
        if (countDown > 1 && !canCrashPlane){ 
            canCrashPlane = true;
        }
    }

    public void StartGame() {
        if (canStartGame && canCrashPlane) {
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
        float i;
        mainAudioMixer.GetFloat("MusicVolume", out i);
        PlayerPrefs.SetFloat("MusicVolumeSaved", i);
        mainAudioMixer.GetFloat("SFXVolume", out i);
        PlayerPrefs.SetFloat("SFXVolumeSaved", i);

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

    public void playButtonOpenSFX() {
        audioManager.Play("ButtonOpen");
    }

    public void playButtonCloseSFX() {
        audioManager.Play("ButtonClose");
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

    //shop functions 
    //EnergyBoost
    public void IncreaseEnergyBoost() {
        float price = Mathf.RoundToInt(100 * PlayerPrefs.GetFloat("energyMultiplier", .7f));
        if (PlayerPrefs.GetInt("energys") - price >= 0 && PlayerPrefs.GetFloat("energyMultiplier", .7f) < 1.9) {
            PlayerPrefs.SetInt("energys", Mathf.RoundToInt(PlayerPrefs.GetInt("energys", 0) - price));
            PlayerPrefs.SetFloat("energyMultiplier", PlayerPrefs.GetFloat("energyMultiplier", .7f) + .13f);

            SetEnergyCounterApearance();
            SetEnergyBarBoostFillApearance();
        }
    }

    public void SetEnergyBarBoostFillApearance() {
        energyBoosterBar.fillAmount = 1 / 1.3f * (PlayerPrefs.GetFloat("energyMultiplier", .7f) - .7f);
        energyBoostCostText.text = Mathf.RoundToInt(100 * PlayerPrefs.GetFloat("energyMultiplier", .7f)).ToString();
    }

    //ScoreMultiplier
    public void IncreaseScoreBoost() {
        float price = Mathf.RoundToInt(100 * PlayerPrefs.GetFloat("ScoreMultiplier", .7f));
        if (PlayerPrefs.GetInt("energys") - price >= 0 && PlayerPrefs.GetFloat("ScoreMultiplier", .7f) < 1.9) {
            PlayerPrefs.SetInt("energys", Mathf.RoundToInt(PlayerPrefs.GetInt("energys", 0) - price));
            PlayerPrefs.SetFloat("ScoreMultiplier", PlayerPrefs.GetFloat("ScoreMultiplier", .7f) + .13f);

            SetEnergyCounterApearance();
            SetScoreBarBoostFillApearance();
        }
    }

    public void SetScoreBarBoostFillApearance() {
        scoreBoosterBar.fillAmount = 1 / 1.3f * (PlayerPrefs.GetFloat("ScoreMultiplier", .7f) - .7f);
        scoreBoostCostText.text = Mathf.RoundToInt(100 * PlayerPrefs.GetFloat("ScoreMultiplier", .7f)).ToString();
    }
    
    public void SetEnergyCounterApearance() {
        energyCounter.text = PlayerPrefs.GetInt("energys", 0).ToString();
    }

    public void PlayRewardedAd() {
        AdsManager.Instance.rewardedAds.ShowRewardedAd();
    }
}
