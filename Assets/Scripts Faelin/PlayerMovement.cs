using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    //movement might need some balancing when the sprites are all scaled correctly

    [SerializeField] private float minTilt;
    [SerializeField] private float maxTilt;
    [SerializeField] private float maxFallingSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private bool canMove = true;
    public bool isBat = false;
    private float dirX;
    private float horizontalInput;
    private bool isPressingSpace;
    public int startEnergys;


    public Rigidbody2D rb;
    public GameObject particles;
    public GameObject SkyFinal;
    public GameObject batDeathParticle;
    public GameObject humanDeathParticle;
    public AudioMixer mixer;
    private GameObject energyBarUI;
    private GameObject energyCanHolder;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Animator deathScreenAnimator;
    private Animator winScreenAnimator;
    private Animator energyBarAnimator;
    private TextMeshProUGUI energysTextWon;
    private TextMeshProUGUI energysTextDied;
    private AudioManager audioManager;
    private CaveEnterTrigger caveEnterTrigger;


    public GameObject particlesWoodBreaking;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        deathScreenAnimator = GameObject.Find("DeathScreen").GetComponent<Animator>();
        winScreenAnimator = GameObject.Find("WinScreen").GetComponent<Animator>();
        energyBarUI = GameObject.Find("EnergyBar");
        energyBarAnimator = energyBarUI.GetComponent<Animator>();
        energyCanHolder = GameObject.Find("EnergyHolder");
        energysTextWon = GameObject.Find("CollectedEnergysWon").GetComponent<TextMeshProUGUI>();
        energysTextDied = GameObject.Find("CollectedEnergysDied").GetComponent<TextMeshProUGUI>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        caveEnterTrigger = GameObject.Find("CaveLightTrigger").GetComponent<CaveEnterTrigger>();

        energyCanHolder.SetActive(false);
        energyBarUI.SetActive(false);

        startEnergys = PlayerPrefs.GetInt("energys", 0);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        InfoAndData.numberOfPlays++;

        audioManager.Play("WindFalling");
    }

    private void Update() {
        CheckInputs();
        if (isBat) {
            HandleJumping();
        }
    }

    private void FixedUpdate() {
        if (canMove) {
            if (!isBat) {
                //gets input of acceleromiter default range is -1 to 1 by default and converts the range to -180 to 180

                if (Application.platform == RuntimePlatform.Android) {
                    rb.velocity = new Vector2(Mathf.Clamp(dirX, minTilt, maxTilt) / 10.6f, Mathf.Max(rb.velocity.y, -maxFallingSpeed));
                }
                else {
                    //pc controls
                    rb.velocity = new Vector2(horizontalInput * 7.5f, Mathf.Max(rb.velocity.y, -maxFallingSpeed));
                }
            }
            else {
                //gets input of acceleromiter default range is -1 to 1 by default and converts the range to -180 to 180

                if (Application.platform == RuntimePlatform.Android) {

                    rb.velocity = new Vector2(Mathf.Clamp(dirX, minTilt, maxTilt) / 10.6f, rb.velocity.y);
                    rb.velocity += new Vector2(rb.velocity.x, -0.5f);
                }
                else {
                    //pc controls
                    rb.velocity += new Vector2(horizontalInput, -0.5f);
                }
            }
        }
    }

    private void CheckInputs() {
        if (Application.platform == RuntimePlatform.Android) {
            dirX = Input.acceleration.x * 180;
            if (dirX < -10f) {
                spriteRenderer.flipX = true;
            }
            else if (dirX > 10f) {
                spriteRenderer.flipX = false;
            }

        }
        else {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            isPressingSpace = Input.GetKeyDown(KeyCode.Space);
            if (horizontalInput < -0.1f) {
                spriteRenderer.flipX = true;
            }
            else if (horizontalInput > 0.1f) {
                spriteRenderer.flipX = false;
            }
        }
    }

    private void HandleJumping() {
        if (Application.platform == RuntimePlatform.Android) {
            Touch touch;
            if (Input.touchCount > 0) {
                touch = Input.GetTouch(0);
            }
            else {
                touch = new Touch();
            }
            if (touch.phase == TouchPhase.Ended) {
                animator.SetTrigger("Flap");
                audioManager.Play("BatFlap");

                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
        }
        else if (isPressingSpace) {
            //pc controls
            animator.SetTrigger("Flap");
            audioManager.Play("BatFlap");

            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
            case "Cloud":
                maxFallingSpeed = maxFallingSpeed / 3;
                break;
            case "Ground":
                if (!isBat) {
                    audioManager.Play("LandInEnergy");
                    audioManager.Stop("WindFallingCave");
                    energyCanHolder.SetActive(true);
                    GameCanvas gameCanvas = GameObject.Find("Canvas").GetComponent<GameCanvas>();
                    gameCanvas.initialYPosition = -880;
                    animator.SetTrigger("Transform");
                    Instantiate(SkyFinal);
                    Instantiate(particles, new Vector3(transform.position.x, transform.position.y - .4f, transform.position.z), Quaternion.Euler(0, 0, 90));
                    canMove = false;
                }
                break;
            case "End":
                Time.timeScale = 0;
                energysTextWon.text = "Collected: " + (PlayerPrefs.GetInt("energys", 0) - startEnergys).ToString();
                winScreenAnimator.SetTrigger("WinScreenIn");
                GameObject.Find("Canvas").GetComponent<GameCanvas>().UpdateScore();
                if (InfoAndData.numberOfPlays >= 3) {
                    InfoAndData.numberOfPlays = 0;
                    AdsManager.Instance.interstitialAds.ShowInterstitialAd();
                }
                break;
            case "Deadly":
                Died();
                break;
            case "TeleportToCenter":
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
                break;
            case "BushTrigger":
                StartCoroutine(ToggleMusicEffects());
                if (!isBat) {
                    audioManager.Stop("WindFalling");
                    audioManager.Play("RustlingBushes");
                    audioManager.Play("WindFallingCave");
                }
                break;
            case "BreakAblePlatform":
                if (isBat) {
                    Died();
                }
                else {
                    switch (Random.Range(1, 3 + 1)) {
                        case 1:
                            audioManager.Play("WoodBreak1");
                            break;
                        case 2:
                            audioManager.Play("WoodBreak2");
                            break;
                        case 3:
                            audioManager.Play("WoodBreak3");
                            break;
                    }
                    Destroy(Instantiate(particlesWoodBreaking, new Vector3(collision.transform.position.x, collision.transform.position.y + 1.75f, collision.transform.position.z), Quaternion.Euler(0, 0, 0)), 5);
                    Destroy(collision.gameObject);
                }

                break;

        }
    }

    private void Died() {
        //generates a random number between 1 and 3 inclusive
        if (Random.Range(1, 101) < 100) {
            switch (Random.Range(1, 3 + 1)) {
                case 1:
                    audioManager.Play("PlayerDeath1");
                    break;
                case 2:
                    audioManager.Play("PlayerDeath2");
                    break;
                case 3:
                    audioManager.Play("PlayerDeath3");
                    break;
            }
        }

        else {
            audioManager.Play("PlayerDeathRare");
        }
        if (caveEnterTrigger.InCave) {
            StartCoroutine(ToggleMusicEffects());
        }
        audioManager.Stop("WindFalling");
        audioManager.Stop("WindFallingCave");
        GameObject particle = Instantiate(isBat ? batDeathParticle : humanDeathParticle, transform.position, transform.rotation);
        particle.transform.localScale = new Vector3(isBat ? (spriteRenderer.flipX ? 1 : -1) : (spriteRenderer.flipX ? -1 : 1), 1, 1);
        particle.transform.rotation = Quaternion.Euler(-90, 0, 0);
        spriteRenderer.color = new Color(0, 0, 0, 0);
        energysTextDied.text = "Collected: " + (PlayerPrefs.GetInt("energys", 0) - startEnergys).ToString();

        GameObject.Find("Canvas").GetComponent<GameCanvas>().UpdateScore();
        Time.timeScale = 0;
        StartCoroutine(DeathScreenIn());
    }

    private void OnTriggerExit2D(Collider2D collision) {
        switch (collision.tag) {
            case "Cloud":
                maxFallingSpeed = maxFallingSpeed * 3;
                break;
        }
    }

    public void SwitchColliders() {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<PolygonCollider2D>().enabled = false;
        energyBarUI.SetActive(true);
        energyBarAnimator.SetTrigger("EnergyBarIn");
        isBat = true;
        canMove = true;
    }

    public IEnumerator DeathScreenIn() {
        yield return new WaitForSecondsRealtime(1f);
        deathScreenAnimator.SetTrigger("DeathScreenIn");
        if (InfoAndData.numberOfPlays >= 3) {
            InfoAndData.numberOfPlays = 0;
            AdsManager.Instance.interstitialAds.ShowInterstitialAd();
        }
    }

    public IEnumerator ToggleMusicEffects() {
        float i;
        mixer.GetFloat("MusicPitch", out i);

        float timeElapsed = 0;
        float targetMusicPitch = i < .95f ? 1f : .9f;
        float currentPitch = i;
        float targetRoomSize = i < .95 ? -10000 : 0;
        mixer.GetFloat("MusicRoomSize", out i);
        float currentRoomSize = i;

        while (timeElapsed < 1) {
            mixer.SetFloat("MusicRoomSize", Mathf.Lerp(currentRoomSize, targetRoomSize, timeElapsed / 1));
            mixer.SetFloat("MusicPitch", Mathf.Lerp(currentPitch, targetMusicPitch, timeElapsed / 1));
            timeElapsed += Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(.01f);
        }

        mixer.GetFloat("MusicPitch", out i);
        mixer.SetFloat("MusicRoomSize", i > .95f ? -10000 : 0);
        mixer.SetFloat("MusicPitch", i > .95f ? 1f : .9f);
    }
}
