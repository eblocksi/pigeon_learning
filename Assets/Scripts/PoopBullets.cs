using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopBullets : MonoBehaviour
{
    [SerializeField] float poopSpeed = 0f;
    [SerializeField] ParticleSystem personDeathNormal;

    ScoreKeeper scoreKeeper;
    People people;
    Rigidbody2D rb;
    AudioPlayer audioPlayer;


    // Determines if the poop animation is finished
    private bool isDone = false;
    // Determines how long the poop has been on the ground
    private float poopTime;
    // After the poop is completed moves the poop to a sorting layer below the targets
    private SpriteRenderer sprite;

    private List<string> enemyTags;
    private SpawnTargets spawnTargets;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        spawnTargets = FindObjectOfType<SpawnTargets>();
        enemyTags = spawnTargets.GetTags();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        rb.velocity = new Vector2(0f, poopSpeed);
    }

    /*
        Calls from within Unity
        Flags as "true" when the animation is over
    */
    void IsDone()
    {
        isDone = true;
        poopTime = Time.time;
        // This changes the sorting layer to above the background, but below the targets
        // So people/cars can drive over the poop
        sprite.sortingLayerName = "FinishedPoop";
    }

    /*
     * * * * NOTES * * * * 
     
    */

    // When pooping animation finishes and it's touching an "Enemy" target
    void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time - poopTime > 0.1f) { return; }

        if (enemyTags.Contains(other.tag) && isDone)
        {
            audioPlayer.PlayGettingHitClip();
            PlayNormalDeath();
            other.gameObject.SetActive(false);
            sprite.sortingLayerName = "Poop";
            isDone = false;
            gameObject.SetActive(false);
        }
    }

    // Return object to pool after it leaves camera view
    private void OnBecameInvisible()
    {
        sprite.sortingLayerName = "Poop";
        isDone = false;
        gameObject.SetActive(false);
    }

    // Particle System
    private void PlayNormalDeath()
    {
        if (personDeathNormal != null)
        {
            ParticleSystem instance = Instantiate(personDeathNormal, transform.position, Quaternion.identity);
            
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
}

