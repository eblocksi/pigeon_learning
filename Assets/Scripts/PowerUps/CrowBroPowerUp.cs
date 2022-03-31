using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBroPowerUp : MonoBehaviour
{
    Player player;
    AudioPlayer audioPlayer;
    private void Awake()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Poop")
            CallCrowBros();
    }

    public void CallCrowBros()
    {
        GameObject _crow = ObjectPooler.SharedInstance.GetPooledObject("CrowBro");
        GameObject _crow2 = ObjectPooler.SharedInstance.GetPooledObject("CrowBro1");
        if (_crow != null && _crow2 != null)
        {
            _crow.SetActive(true);
            _crow2.SetActive(true);
        }
    }
}
