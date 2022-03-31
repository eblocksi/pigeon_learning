using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Simple : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minDistance = 30f;
    public float fireRateMin = 0.2f;
    public float fireRateMax = 1.5f;

    private float fireRate;

    Rigidbody2D rb;
    AudioPlayer audioPlayer;
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(EnemyShooting());
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(0, moveSpeed * Time.deltaTime);
    }

    IEnumerator EnemyShooting()
    {
        while (true)
        {
            float dist = transform.position.y - player.transform.position.y;
            Debug.Log(dist);
            if (dist < minDistance)
            {
                fireRate = Random.Range(fireRateMin, fireRateMax);

                GameObject _enemyPoop = ObjectPooler.SharedInstance.GetPooledObject("Enemy_Attack1");
                _enemyPoop.transform.position = transform.position;
                _enemyPoop.SetActive(true);
            }
            yield return new WaitForSeconds(fireRate);
        }
    }
}
