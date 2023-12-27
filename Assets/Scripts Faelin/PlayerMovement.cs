using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
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


    public Rigidbody2D rb;
    public GameObject particles;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        CheckInputs();
        if (isBat) {
            HandleJumping();
        }

        if (Input.GetKey(KeyCode.T)) {
            Time.timeScale = 10;
        }
        else {
            Time.timeScale = 1;
        }
    }

    private void FixedUpdate() {
        if (canMove) {
            if (!isBat) {
                //gets input of acceleromiter default range is -1 to 1 by default and converts the range to -180 to 180

                if (Application.platform == RuntimePlatform.Android) {

                    GameObject.Find("DebugText").GetComponent<TextMeshProUGUI>().text = Mathf.Clamp(dirX, minTilt, maxTilt) / 10.6f + "";

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
            spriteRenderer.flipX = dirX < 0;
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

                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
        }
        else if (isPressingSpace) {
            //pc controls
            animator.SetTrigger("Flap");

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
                    animator.SetTrigger("Transform");
                    Instantiate(particles, new Vector3(transform.position.x, transform.position.y - .4f, transform.position.z), Quaternion.Euler(0, 0, 90));
                    canMove = false;
                }
                break;
        }
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
        isBat = true;
        canMove = true;
    }
}
