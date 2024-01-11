using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;

    public static AudioManager instance;
    public AudioMixer mixer;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else { 
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start() {
        foreach (Sound s in sounds) {
            if (s.playOnStart) {
                Play(s.name);
            }
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.Log("couldnt find " + name);
            return;
        }

        //if the sound is music it plays it in the audiomixer Main under the group of Music and if not it plays it under the group Sfx
        s.source.outputAudioMixerGroup = s.isMusic ? mixer.FindMatchingGroups("Music")[0] : mixer.FindMatchingGroups("Sfx")[0];

        if (s.oneShot) {
            s.source.PlayOneShot(s.clip);
        }
        else {
            s.source.Play();
        }
    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.Log("couldnt find " + name);
            return;
        }
        s.source.Stop();
    }
}
