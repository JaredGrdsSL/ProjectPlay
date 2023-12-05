using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {
    [SerializeField] private float minTilt;
    [SerializeField] private float maxTilt;
    public float maxFallingSpeed;

    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();

        //if (SystemInfo.supportsGyroscope) {
        //    Input.gyro.enabled = true;
        //}
        //else {
        //    Debug.Log("Doesnt Support Gyroscope");
        //}
    }

    private void FixedUpdate() {
        //float xRotation = Input.gyro.attitude.eulerAngles.x;
        float dirX = Input.acceleration.x * 180;
        //RaycastHit2D hit2D = new RaycastHit2D(transform.position, )
        
        if (Application.platform == RuntimePlatform.Android) {
            GameObject.Find("DebugText").GetComponent<TextMeshProUGUI>().text = Mathf.Clamp(dirX, minTilt, maxTilt) / 10.6f + "";
            rb.velocity = new Vector2(Mathf.Clamp(dirX, minTilt, maxTilt) / 10.6f, Mathf.Max(rb.velocity.y, -maxFallingSpeed));
        }
        else {
            //pc controls
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 7.5f, Mathf.Max(rb.velocity.y, -maxFallingSpeed));
        }
    }
}
