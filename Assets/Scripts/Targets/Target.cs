using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float moveSpeed = -10f;
    [SerializeField] int points = 10;

    Player player;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    ScoreKeeper scoreKeeper;
    AudioSource audioSource;

    public AudioClip gettingHitClip;

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
        rb.velocity = new Vector2(0, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "FinishedPoop" && !isKilled)
        {
            scoreKeeper.ModifyScore(points);
            GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            StartCoroutine(PlayPersonDeathSound());
            other.gameObject.SetActive(false);
        }
    }

    IEnumerator PlayPersonDeathSound()
    {
        isKilled = true;
        audioSource.clip = gettingHitClip;
        audioSource.Play();
        while(audioSource.isPlaying)
            yield return null;
        gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
