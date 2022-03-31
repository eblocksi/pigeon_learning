using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBros : MonoBehaviour
{
    [Header("Crow Poop Gun")]
    [SerializeField]
    Transform poopGun;

    public float crowXLane;
    public float crowTime = 9f;
    public float enterExitSpeed = 20f;
    private float crowMoveSpeed = 7f;
    public float fireRate = 0.15f;
    public bool leftCrow;
    
    private Player player;
    private Rigidbody2D rb;
    private AudioPlayer audioPlayer;

    private float spawnTime;
    private float lastShot;
    private bool isCrowing;
    private bool isPooping;
    private float goalY;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // NOTE CHANGE THE SOUND CLIP
        //audioPlayer.PlayPoopingClip();
        goalY = player.transform.position.y;
        spawnTime = Time.time;
        transform.position = new Vector2(crowXLane, player.transform.position.y - 10);
    }

    private void FixedUpdate()
    {
        if (Time.time - spawnTime > crowTime)
        {
            isCrowing = false;
            CrowLeaves();
        }

        if (isCrowing)
        {
            transform.Translate(0, crowMoveSpeed * Time.deltaTime, 0);
            //if (Time.time - lastShot > fireRate)
            //{
            //    lastShot = Time.time;
            //    CrowPoop();
            //}
        }
        else
        {
            transform.Translate(0, enterExitSpeed * Time.deltaTime, 0);
            if (transform.position.y >= goalY + 10f)
            {
                isCrowing = true;
                lastShot = Time.time;
            }
                
        }
    }

    //public void CrowArrives()
    //{
    //    GameObject _crow = ObjectPool.SharedInstance.GetPooledObject("CrowBro");
    //    if (_crow != null)
    //    {
    //        // NOTE CHANGE THE SOUND CLIP
    //        audioPlayer.PlayPoopingClip();
    //        if (leftCrow)
    //        {
    //            _crow.transform.position = new Vector2(player.transform.position.x - 10, -6.75f);
    //            _crow.SetActive(true);
    //        }
    //    }
    //    goalX = player.transform.position.x;
    //    spawnTime = Time.time;
    //}

    public void CrowLeaves()
    {
        isCrowing = false;
        goalY = player.transform.position.y + 50;
    }

    private void CrowPoop()
    {
        GameObject _poop = ObjectPooler.SharedInstance.GetPooledObject("Poop");
        if (_poop != null)
        {
            //audioPlayer.PlayPoopingClip();
            _poop.transform.position = poopGun.transform.position;
            _poop.transform.rotation = poopGun.transform.rotation;
            _poop.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Poop")
        {
            CrowPoop();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPooping = false;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
