using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Poop Gun")]
    [SerializeField] Transform poopGun;

    public bool isDead;

    AudioSource audioSource;
    LevelManager levelManger;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        levelManger = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        isDead = false;
    }

    //Firing
    public void Poop()
    {
        GameObject _poop = ObjectPooler.SharedInstance.GetPooledObject("Poop");
        if (_poop != null)
        {
            _poop.transform.position = poopGun.transform.position;
            _poop.transform.rotation = poopGun.transform.rotation;
            _poop.SetActive(true);
            audioSource.Play();
        }
    }

    public void Shoot()
    {
        GameObject _poop = ObjectPooler.SharedInstance.GetPooledObject("PoopAttack");
        if (_poop != null)
        {
            _poop.transform.position = poopGun.transform.position;
            _poop.transform.rotation = poopGun.transform.rotation;
            _poop.SetActive(true);
            audioSource.Play();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy_Attack1" && !isDead)
        {
            isDead = true;
            levelManger.LoadGameOver();
            other.gameObject.SetActive(false);
        }
    }
}