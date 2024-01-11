using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTutorial : MonoBehaviour
{
    public void Ended() {
        Destroy(gameObject);
    }
}
