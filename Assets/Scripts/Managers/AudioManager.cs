using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public Sound[] sounds;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else instance = this;
    }

    private void Start()
    {
        InitSounds();
    }

    public void PlaySound(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        if (sound != null) sound.source.Play();
    }

    public void StopSound(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        if (sound != null) sound.source.Stop();
    }

    public void StopAllSounds()
    {
        foreach (Sound sound in sounds) StopSound(sound.name);
    }

    public bool IsPlaying(string name)
    {
        bool playing = true;
        Sound sound = Array.Find(sounds, s => s.name == name);
        if (sound != null) playing = sound.source.isPlaying;
        return playing;
    }

    public void InitSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
        }
    }

}
