using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip GameOverSFX;
    [SerializeField] private AudioClip ButtonClickSFX;
    [SerializeField] private AudioClip ClearSFX;
    [SerializeField] private AudioClip PlaceSFX;

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float sfxVolume = 0.7f;

    public static SFX Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.volume = sfxVolume;
        audioSource.playOnAwake = false;
    }

    public void PlayGameOverSFX()
    {
        PlaySound(GameOverSFX);
    }

    public void PlayButtonClickSFX()
    {
        PlaySound(ButtonClickSFX);
    }

    public void PlayClearSFX()
    {
        PlaySound(ClearSFX);
    }

    public void PlayPlaceSFX()
    {
        PlaySound(PlaceSFX);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    public void SetVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (audioSource != null)
        {
            audioSource.volume = sfxVolume;
        }
    }
}