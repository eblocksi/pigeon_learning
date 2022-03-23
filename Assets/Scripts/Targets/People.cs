using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour
{
    [SerializeField] float personMoveSpeed = -10f;

    Player player;
    Rigidbody2D rb;
    ScoreKeeper scoreKeeper;

    private int points = 10;
    private bool isKilled;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        player = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(0, personMoveSpeed * Time.deltaTime);
    }

    private void OnDestroy()
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
        Destroy(gameObject);
    }
}
