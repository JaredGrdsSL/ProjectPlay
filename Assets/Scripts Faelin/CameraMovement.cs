using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] private float batCameraOffset;
    [SerializeField] private float playerCameraOffset;

    private PlayerMovement playerMovement;

    public SpriteRenderer wallSprite;



    void Start() {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Debug.Log(wallSprite.bounds.size.x);
    }

    void Update() {

        if (!playerMovement.isBat) {
            UpdateCamera(playerCameraOffset);
        }
        else if (playerMovement.gameObject.transform.position.y > transform.position.y - batCameraOffset) {
            UpdateCamera(batCameraOffset);
        }
    }

    private void UpdateCamera(float offset) {
        if (transform.position.y > -440.2991f && !playerMovement.isBat) {
            transform.position = new Vector3(transform.position.x, playerMovement.gameObject.transform.position.y + offset, transform.position.z);
        }
        else if (playerMovement.isBat) {
            transform.position = new Vector3(transform.position.x, playerMovement.gameObject.transform.position.y + offset, transform.position.z);
        }
    }
}
