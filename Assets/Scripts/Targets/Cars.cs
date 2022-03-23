using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour
{
    [SerializeField] float carMoveSpeed = -20f;

    Player player;
    ScoreKeeper scoreKeeper;
    Rigidbody2D rb;

    private int points = 25;
    private bool isKilled;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        player = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(0, carMoveSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        if (player != null)
        {
            if (player.transform.position.y - transform.position.y < 5f)
            {
                scoreKeeper.ModifyScore(points);
            }
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
