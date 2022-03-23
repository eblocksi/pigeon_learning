using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    [SerializeField] float personMoveSpeed = -1f;
    [SerializeField] float carMoveSpeedSouth = -2f;
    [SerializeField] ScoreKeeper scoreKeeper;

    Rigidbody2D rb;

    private float moveSpeed = -1f;
    private float points = 10f;


    void Start()
    {
        //spawnTarget = GetComponent<SpawnTargets>();
        rb = GetComponent<Rigidbody2D>();
        //StartMoving();
    }


    void Update()
    {
        rb.velocity = new Vector2(0, moveSpeed);
    }

    private void Move()
    {
        
    }

    //void StartMoving()
    //{
    //    if (spawnTarget.IsCar())
    //    {
    //        if (spawnTarget.FaceSouth())
    //            moveSpeed = carMoveSpeedSouth;
    //        else
    //            moveSpeed = carMoveSpeedNorth;
    //    }
    //    else
    //        moveSpeed = personMoveSpeed;

    //}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    //Debug.Log(other.name);
    //    if (other.gameObject.CompareTag("Poop"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
