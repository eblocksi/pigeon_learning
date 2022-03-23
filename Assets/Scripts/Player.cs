using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Poop Gun")]
    [SerializeField] Transform poopGun;

    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    //Firing
    public void Poop()
    {
        GameObject _poop = ObjectPool.SharedInstance.GetPooledObject("Poop");
        if (_poop != null)
        {
            audioPlayer.PlayPoopingClip();
            _poop.transform.position = poopGun.transform.position;
            _poop.transform.rotation = poopGun.transform.rotation;
            _poop.SetActive(true);
        }
    }
}