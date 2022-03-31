using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float superSpeedTime = 5f;
    public float superSpeedSpeed = 30f;
    public float superSpeedCameraSpeed = 20f;

    private InputManager player;
    private CameraFollow cameraFollowScript;

    private bool isBoosting;
    private float startTime;
    private float defaultMoveSpeed;
    private float defaultCameraSpeed;

    private void Awake()
    {
        player = FindObjectOfType<InputManager>();
        cameraFollowScript = FindObjectOfType<CameraFollow>();
    }

    private void Start()
    {
        defaultMoveSpeed = player.moveSpeed;
        defaultCameraSpeed = cameraFollowScript.cameraSpeed;
    }

    private void FixedUpdate()
    {
        if (isBoosting && (Time.time - startTime < superSpeedTime))
        {
            cameraFollowScript.cameraSpeed = superSpeedCameraSpeed;
            player.moveSpeed = superSpeedSpeed;
        }
        else
        {
            player.moveSpeed = defaultMoveSpeed;
            cameraFollowScript.cameraSpeed = defaultCameraSpeed;
            isBoosting = false;
        }
            
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Poop")
        {
            StartBoost();
        }
    }

    public void StartBoost()
    {
        startTime = Time.time;
        isBoosting = true;
    }
}
