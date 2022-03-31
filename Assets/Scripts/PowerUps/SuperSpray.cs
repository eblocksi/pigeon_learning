using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpray : MonoBehaviour
{
    public float superSprayTime = 5f;
    public float superSprayFireRate = 0.2f;

    private Player player;
    private bool isPooping;
    private float startTime;
    private float fireTime;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        if (isPooping && (Time.time - startTime < superSprayTime))
        {
            if (Time.time - fireTime > superSprayFireRate)
            {
                player.Poop();
                fireTime = Time.time;
            }
        }
        else
            isPooping = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Poop")
        {
            StartSuperSpray();
        }
    }

    public void StartSuperSpray()
    {
        startTime = Time.time;
        fireTime = Time.time;
        isPooping = true;
    }
}
