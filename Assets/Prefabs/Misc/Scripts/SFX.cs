using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    AudioSource source;
    bool isPlay = false;
    float maxDur = 99f;
    void Update()
    {
        if (isPlay && source != null)
        {
            if (!source.isPlaying) Destroy(gameObject);
            maxDur -= Time.deltaTime;
            if (maxDur < 0f) Destroy(gameObject);
        }
    }
    public void initSFX(AudioClip cli, float pitchshift, float volume, float maxdur) //Run after creating prefab
    {
        source = GetComponent<AudioSource>();
        source.clip = cli;
        source.pitch = Random.Range(1 - pitchshift, 1 + pitchshift);
        source.volume = volume;
        source.Play();
        source.loop = false;
        isPlay = true;
        maxDur = maxdur;
    }
    public void initSFX(AudioClip clip, float pitchshift)
    {
        initSFX(clip, 0f,1f,99f);
    }
    public void initSFX(AudioClip clip)
    {
        initSFX(clip,0f,1f,99f);
    }
}
