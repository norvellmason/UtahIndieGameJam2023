using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioFade
{

    public static IEnumerator FadeOutAndIn(AudioSource audioSource, AudioClip nextClip, float fadeTime, float fadeInVolume)
    {
        yield return FadeOut(audioSource, fadeTime);

        float currentMusicPosition = audioSource.time;
        audioSource.Stop();

        audioSource.clip = nextClip;
        audioSource.time = currentMusicPosition;
        audioSource.Play();

        yield return FadeIn(audioSource, fadeTime, fadeInVolume);
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            Debug.Log("FadeOut: " + audioSource.volume.ToString());

            yield return null;
        }
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float fadeTime, float fadeInVolume)
    {
        float startVolume = 0.1f;

        while (audioSource.volume < fadeInVolume) 
        { 
            audioSource.volume += startVolume * Time.deltaTime;
            Debug.Log("FadeIn: " + audioSource.volume.ToString());

            yield return null;
        }
    }
}