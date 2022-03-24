using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnTargets : MonoBehaviour
{
    [Header("People")]
    [SerializeField] List<string> peopleTags;

    [Header("Cars")]
    [SerializeField] List<string> carTags;

    [Header("General")]
    public float minSpawnTime = .5f;
    public float maxSpawnTime = 3f;
    public float leftLaneX = -6.75f;
    public float rightLaneX = 6.75f;
    public float middleLaneX = 0f;
    public bool isLooping;

    private Player player;
    private float spawnTime;
    private bool isCar;

    

    // Const
    const float northYStartCoord = 17f;

    void Start()
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(SpawnTargerts());
    }

    IEnumerator SpawnTargerts()
    {
        do
        {
            float spawnLane = GetSpawnLane();
            
            //Debug.Log("spawnLane 1: " + spawnLane.Item1 + " - 2: " + spawnLane.Item2);
            //Debug.Log("spawnlane 3: " + spawnLane.Item3 + " - 4: " + spawnLane.Item4);
            if (isCar)
            {
                int rnd = Random.Range(0, carTags.Count);
                GameObject _car = ObjectPool.SharedInstance.GetPooledObject(carTags[rnd]);

                if (_car != null)
                {
                    _car.transform.position = new Vector2(0, player.transform.position.y + 8 + northYStartCoord);
                    _car.SetActive(true);
                    isCar = false;
                }
            }
            else
            {
                int rnd = Random.Range(0, peopleTags.Count);
                GameObject _person = ObjectPool.SharedInstance.GetPooledObject(peopleTags[rnd]);

                if (_person != null)
                {
                    _person.transform.position = new Vector2(spawnLane, player.transform.position.y + 8 + northYStartCoord);
                    _person.SetActive(true);
                }
            }
            spawnTime = GetSpawnTime();

            yield return new WaitForSeconds(spawnTime);
        } while (isLooping);

    }

    private float GetSpawnLane()
    {
        int randLane = Random.Range(1, 4);

        // Left Lane
        if (randLane == 1)
        {
            isCar = false;
            return leftLaneX;
        }

        if (randLane == 2)
        {
            isCar = false;
            return rightLaneX;
        }

        isCar = true;
        return middleLaneX;
    }

    private float GetSpawnTime()
    {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }    

    public List<string> GetTags()
    {
        return peopleTags.Concat(carTags).ToList();
    }
}
