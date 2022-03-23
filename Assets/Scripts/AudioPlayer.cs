using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Pooping")]
    [SerializeField] AudioClip poopingClip;
    [SerializeField][Range(0f, 1f)] float poopVolume = 1f;

    [Header("Getting Hit")]
    [SerializeField] AudioClip owClip;
    [SerializeField][Range(0f, 1f)] float owVolume = 1f;

    [Header("Menu")]
    [SerializeField] AudioClip menuSelectClip;
    [SerializeField][Range(0f, 1f)] float menuSelectVolume = 1f;

    static AudioPlayer instance;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayPoopingClip()
    {
        PlayClip(poopingClip, poopVolume);
    }

    public void PlayGettingHitClip()
    {
        PlayClip(owClip, owVolume);
    }

    public void PlayMenuSelectClip()
    {
        audioSource.Stop();
        PlayClip(menuSelectClip, menuSelectVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
