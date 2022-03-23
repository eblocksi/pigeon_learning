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
        GameObject _poop = ObjectPool.SharedInstance.GetPooledObject();
        if (_poop != null)
        {
            audioPlayer.PlayPoopingClip();
            _poop.transform.position = poopGun.transform.position;
            _poop.transform.rotation = poopGun.transform.rotation;
            _poop.tag = "Poop";
            _poop.SetActive(true);
        }
        //GameObject _poop = Instantiate(bulletPoop, poopGun.position, transform.rotation);
        //_poop.tag = "Poop";
    }
}
//private void Update()
    //{
    //    //InitBounds();
    //    //Move();
    //}


    // Old movement style with keyboard
    //private void Move()
    //{
    //    Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
    //    Vector2 newPos = new Vector2();

    //    newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
    //    newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
    //    transform.position = newPos;
    //}

    //private void OnMove(InputValue value)
    //{
    //    rawInput = value.Get<Vector2>();
    //}

    //void InitBounds()
    //{
    //    Camera mainCamera = Camera.main;

    //    minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
    //    maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    //}