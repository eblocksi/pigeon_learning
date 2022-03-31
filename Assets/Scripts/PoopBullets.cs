using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopBullets : MonoBehaviour
{
    [SerializeField] float poopSpeed = 0f;
    [SerializeField] ParticleSystem personDeathNormal;

    public List<string> enemyTags;

    ScoreKeeper scoreKeeper;
    Rigidbody2D rb;


    // Determines if the poop animation is finished
    private bool isDone = false;
    // Determines if it's aimed at a target or enemy
    private bool isBullet = false;
    // Determines how long the poop has been on the ground
    private float poopTime;
    // After the poop is completed moves the poop to a sorting layer below the targets
    private SpriteRenderer sprite;

    private List<string> targetTags;
    private SpawnTargets spawnTargets;

    private void Awake()
    {
        spawnTargets = FindObjectOfType<SpawnTargets>();
        targetTags = spawnTargets.GetTags();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        rb.velocity = new Vector2(0f, poopSpeed);

        // If the poop animation has been finished, and it didn't hit a person
        // Change the tag back so it doesn't trigger a successful hit
        if (Time.time - poopTime > 0.1f)
            sprite.sortingLayerName = "Poop";
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
        if (poopSpeed < 5f)
            sprite.sortingLayerName = "FinishedPoop";
    }

    /*
     * * * * NOTES * * * * 
     
    */

    // When pooping animation finishes and it's touching an "Enemy" target
    //void OnTriggerStay2D(Collider2D other)
    //{
    //    // Determine if it's a poop at enemy, or poop at target
    //    if (poopSpeed > 5f) { return; }

    //    // If the poop animation hasn't finished yet
    //    if (Time.time - poopTime > 0.1f) { return; }


    //    //if (targetTags.Contains(other.tag) && isDone)
    //    //{
    //    //    PlayNormalDeath();

    //    //    sprite.sortingLayerName = "Poop";
    //    //    isDone = false;
    //    //    gameObject.SetActive(false);
    //    //}
    //}

    // Return object to pool after it leaves camera view

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyTags.Contains(other.gameObject.tag))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
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

    public void IsBullet()
    {
        isBullet = true;
    }

    public void IsPoop()
    {
        isBullet = false;
    }
}

