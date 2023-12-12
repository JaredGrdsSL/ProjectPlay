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
        Play("theme");
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.Log("couldnt find " + name);
            return;
        }

        s.source.outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[0];

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
