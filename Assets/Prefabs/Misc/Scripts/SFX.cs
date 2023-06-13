using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    AudioSource source;
    bool isPlay = false;
    void Update()
    {
        if (isPlay && source != null)
        {
            if (!source.isPlaying) Destroy(gameObject);
        }
    }

    public void initSFX(AudioClip cli, float pitchshift, float volume) //Run after creating prefab
    {
        source = GetComponent<AudioSource>();
        source.clip = cli;
        source.Play();
        isPlay = true;
    }
    public void initSFX(AudioClip clip, float pitchshift)
    {
        initSFX(clip, 1f,1f);
    }
    public void initSFX(AudioClip clip)
    {
        initSFX(clip,1f,1f);
    }
}
