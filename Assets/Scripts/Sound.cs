using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    // Start is called before the first frame update
    public AudioClip clip;
    public string name;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop;
    public bool oneShot;

    [HideInInspector]
    public AudioSource source;
}
