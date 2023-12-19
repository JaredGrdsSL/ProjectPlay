using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class settingsmanager : MonoBehaviour
{
    public AudioMixer audioMusic;
    public AudioMixer audioSound;
    public void SetVolume(float volume)
    {
        audioSound.SetFloat("volume", volume);
    }
    public void SetMusic(float music)
    {
        audioMusic.SetFloat("music", music);
    }
}
