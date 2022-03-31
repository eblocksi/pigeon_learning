using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour
{
    [SerializeField] float carMoveSpeed = -20f;

    Player player;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    ScoreKeeper scoreKeeper;
    AudioSource audioSource;

    public AudioClip gettingHitClip;

    private int points = 25;
    private bool isKilled;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
        player = FindObjectOfType<Player>();
        isKilled = false;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(0, carMoveSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
