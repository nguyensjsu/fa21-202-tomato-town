using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager instance { get { return _instance; } }

    AudioSource[] sfx;
    public AudioClip[] sounds; // Hardcoded for now

    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(gameObject);
        if(_instance != null && _instance != this)
            Destroy(gameObject);
        else { _instance = this; }

        sfx = GetComponentsInChildren<AudioSource>();
        UpdateVolume();
    }

    // Adjust volumes by settings
    public void UpdateVolume() {
        for(int i = 0; i < sfx.Length; i++) {
            sfx[i].volume = 30f / 100;
        }
    }

    // Play sound effects
    public void PlayKeyPress() { PlaySFX(sounds[0]); }
    public void PlayHurt() { PlaySFX(sounds[2]); }
    public void PlayHurtE() { PlaySFX(sounds[1]); }
    public void PlayJump() { PlaySFX(sounds[3]); }
    public void PlayThrow() { PlaySFX(sounds[4]); }
    public void PlayPickup() { PlaySFX(sounds[5]); }

    private void PlaySFX(AudioClip sound) {
        for(int i = 0; i < sfx.Length; i++) {
            if(!sfx[i].isPlaying) {
                sfx[i].clip = sound;
                sfx[i].Play();
                return;
            }
        }
    }
}
