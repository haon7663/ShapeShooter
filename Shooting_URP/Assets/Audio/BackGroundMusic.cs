using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackGroundMusic : MonoBehaviour
{
    public static BackGroundMusic instance;

    public AudioClip[] m_AudioClips;
    public AudioMixerGroup m_MixerGroup;

    private AudioSource[] audioSources;

    private void Awake()
    {
        var obj = FindObjectsOfType<BackGroundMusic>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        audioSources = new AudioSource[m_AudioClips.Length];
        for (int i = 0; i < m_AudioClips.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].playOnAwake = true;
            audioSources[i].loop = true;
            audioSources[i].clip = m_AudioClips[i];
            audioSources[i].outputAudioMixerGroup = m_MixerGroup;
            audioSources[i].volume = 0;
        }
        Switch(0);
        Play();
    }

    public void Switch(int index)
    {
        for (int i = 0; i < m_AudioClips.Length; i++)
        {
            StartCoroutine(AudioFade(audioSources[i], i == index ? 1 : 0));
        }
    }

    public void Play()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.PlayScheduled(AudioSettings.dspTime + 0.2f);
        }
    }

    public void Stop()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }
    IEnumerator AudioFade(AudioSource audio, float volume)
    {
        float time = 0;
        float start = audio.volume;

        while (time < 1)
        {
            time += Time.deltaTime;
            audio.volume = Mathf.Lerp(start, volume, time / 1);
            yield return null;
        }
        audio.volume = volume;
        yield break;
    }
}
