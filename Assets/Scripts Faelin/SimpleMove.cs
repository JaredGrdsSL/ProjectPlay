using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {
    public enum Direction {
        horizontal,
        vertical
    }

    [SerializeField] private float speed;
    [SerializeField] private Direction direction;

    void FixedUpdate() {
        switch (direction) {
            case Direction.horizontal:
                transform.position = new Vector2(transform.position.x + speed, transform.position.y);
                break;
            case Direction.vertical:
                transform.position = new Vector2(transform.position.x, transform.position.y + speed);
                break;
            default:
                transform.position = new Vector2(transform.position.x + speed, transform.position.y);
                break;
        }
    }
}
