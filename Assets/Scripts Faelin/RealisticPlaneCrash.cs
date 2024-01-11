using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealisticPlaneCrash : MonoBehaviour
{
    public void PlayAudio() {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("RealisticCrash");
    }

    public void EndOfAnimation() {
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }
}
