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
        source.pitch = Random.Range(1 - pitchshift, 1 + pitchshift);
        source.Play();
        source.loop = false;
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
