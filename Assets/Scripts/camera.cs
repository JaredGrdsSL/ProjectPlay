using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class camera : MonoBehaviour
{
    public UnityEngine.Transform player; // Explicitly specify the Transform from UnityEngine
    public float verticalOffset = 2.0f;

    void LateUpdate()
    {
        if (player != null) // Check if the player reference is not null
        {
            Vector3 playerPos = player.position;
            playerPos.x = transform.position.x;
            playerPos.y += verticalOffset;
            transform.position = playerPos;
        }
    }
}
