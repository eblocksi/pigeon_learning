using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    //[Header("Poop")]
    //[SerializeField] GameObject bulletPoop;
    //[SerializeField] Transform poopGun;
    [Header("Player")]
    Player player;

    [Header("Movement")]
    public float moveSpeed;
    public float tapTime = 0.2f;
    public float tapDistance = 50f;

    // Used to measure how long the finger was pressed
    private float startTime;

    private float moveDistance = 6.75f;
    private int currentLane = 1;
    private int desiredLane = 1;
    private bool changingLanes;

    // Touch Controls
    private TouchControls touchControls;
    private Vector2 startPosition;
    private Vector2 endPosition;


    private void Awake()
    {
        touchControls = new TouchControls();
        player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }


    private void Start()
    {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void FixedUpdate()
    {
        if (desiredLane < currentLane)
            MoveLeft();
        else if (desiredLane > currentLane)
            MoveRight();
    }

    private void StartTouch(InputAction.CallbackContext ctx)
    {
        //Debug.Log("Start touch at: " + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        startTime = (float)ctx.time;
        startPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
    }

    private void EndTouch(InputAction.CallbackContext ctx)
    {
        //Debug.Log("End touch at: " + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        endPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        if ((float)ctx.time - startTime > tapTime && Mathf.Abs(startPosition.x - endPosition.x) > tapDistance)
        {
            if (!changingLanes)
                Move();
        }
        else
            player.Poop();
    }

    private void Move()
    {
        if (endPosition.x < startPosition.x)
        {
            desiredLane--;
            if (desiredLane < 0)
                desiredLane = 0;
        }
        if (endPosition.x > startPosition.x)
        {
            desiredLane++;
            if (desiredLane > 2)
                desiredLane = 2;
        }
        Debug.Log("current lane: " + currentLane + " Desired Lane: " + desiredLane);

    }

    private void MoveLeft()
    {
        if (desiredLane == 1)
        {
            if (transform.position.x > 0)
            {
                changingLanes = true;
                transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                currentLane = 1;
                transform.position = new Vector2(0, transform.position.y);
                changingLanes = false;
            }
        }
        else if (desiredLane == 0)
        {
            if (transform.position.x > -6.75f)
            {
                changingLanes = true;
                transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                currentLane = 0;
                transform.position = new Vector2(-6.75f, transform.position.y);
                changingLanes = false;
            }
        }
            
    }
    private void MoveRight()
    {
        if (desiredLane == 1)
        {
            if (transform.position.x < 0)
            {
                changingLanes = true;
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                currentLane = 1;
                transform.position = new Vector2(0, transform.position.y);
                changingLanes = false;
            }
        }
        else if (desiredLane ==  2)
        {
            if (transform.position.x < 6.75f)
            {
                changingLanes = true;
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                currentLane = 2;
                transform.position = new Vector2(6.75f, transform.position.y);
                changingLanes = false;
            }
        }
    }
}
