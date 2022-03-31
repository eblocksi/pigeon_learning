using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullets : MonoBehaviour
{
    [SerializeField]
    float enemyPoopSpeed = -10f;

    private List<string> targetTags;

    Player player;
    Rigidbody2D rb;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(0f, enemyPoopSpeed);
    }
}
