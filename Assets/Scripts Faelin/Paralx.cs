using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralx : MonoBehaviour
{
    public float depth;
    PlayerMovement player;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void FixedUpdate() {
        float realVelocity = player.rb.velocity.y / depth;
        Vector2 pos = transform.position;

        pos.y += realVelocity * Time.fixedDeltaTime;

        transform.position = new Vector2(transform.position.x, pos.y);
    }
}
