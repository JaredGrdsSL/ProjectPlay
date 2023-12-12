using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {
    //movement might need some balancing when the sprites are all scaled correctly

    [SerializeField] private float minTilt;
    [SerializeField] private float maxTilt;
    [SerializeField] private bool isBat = false;
    public float maxFallingSpeed;
    private float dirX;
    private float horizontalInput;
    private bool isPressingSpace;


    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        CheckInputs();
        if (isBat) {
            HandleJumping();
        }
    }

    private void FixedUpdate() {
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

    private void CheckInputs() {
        dirX = Input.acceleration.x * 180;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isPressingSpace = Input.GetKeyDown(KeyCode.Space);
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
                rb.velocity = new Vector2(rb.velocity.x, 10);
            }
        }
        else if (isPressingSpace) {
            //pc controls
            rb.velocity = new Vector2(rb.velocity.x, 10);
        }
    }
}
