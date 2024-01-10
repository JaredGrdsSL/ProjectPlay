using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CaveEnterTrigger : MonoBehaviour
{
    private Light2D globalLight;
    private Light2D playerLight;
    private Light2D playerLightSmall;
    public bool InCave;

    private void Start() {
        globalLight = GameObject.Find("GlobalLight").GetComponent<Light2D>();
        playerLight = GameObject.Find("PlayerLight").GetComponent<Light2D>();
        playerLightSmall = GameObject.Find("PlayerLightSmall").GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Destroy(GameObject.Find("SkyObsticles"));
            InCave = !InCave;
            StartCoroutine(ToggleLights(InCave));
        }
    }

    private IEnumerator ToggleLights(bool isEnter) {
        float targetLightGlobal = isEnter ? .05f : 1;
        float targetLightLocal = isEnter ? .5f : 0;
        float targetLightLocalSmall = isEnter ? .3f : 0;
        float startLightGlobal = globalLight.intensity;
        float startLightLocal = playerLight.intensity;
        float startLightLocalSmall = playerLightSmall.intensity;
        float transitionTime = 1f;
        float timeElapsed = 0;

        while (timeElapsed < transitionTime) {
            globalLight.intensity = Mathf.Lerp(startLightGlobal, targetLightGlobal, timeElapsed / transitionTime);
            playerLight.intensity = Mathf.Lerp(startLightLocal, targetLightLocal, timeElapsed / transitionTime);
            playerLightSmall.intensity = Mathf.Lerp(startLightLocalSmall, targetLightLocalSmall, timeElapsed / transitionTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        globalLight.intensity = targetLightGlobal;
        playerLight.intensity = targetLightLocal;
        playerLightSmall.intensity = targetLightLocalSmall;
    }
}
