using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] private float batCameraOffset;
    [SerializeField] private float playerCameraOffset;

    private PlayerMovement playerMovement;

    void Start() {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update() {
        if (!playerMovement.isBat) {
            UpdateCamera(playerCameraOffset);
        }
        else if (playerMovement.gameObject.transform.position.y > transform.position.y - batCameraOffset){
            UpdateCamera(batCameraOffset);
        }
    }

    private void UpdateCamera(float offset) {
        transform.position = new Vector3(transform.position.x, playerMovement.gameObject.transform.position.y + offset, transform.position.z);
    }
}
