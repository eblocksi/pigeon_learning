using System.Collections;
using System.Collections.Generic;
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
    public float peopleLeftLaneX = -6.75f;
    public float peopleRightLaneX = 6.75f;
    public float carX = 0f;

    private float spawnTime;
    public bool isLooping;

    // Const
    const float northYStartCoord = 17f;

    void Start()
    {
            StartCoroutine(SpawnTargerts());
    }

    IEnumerator SpawnTargerts()
    {
        do
        {
            var spawnLane = GetSpawnLane();
            int spawnIndex = Random.Range(0, spawnLane.Item3);
            Debug.Log("spawnLane 1: " + spawnLane.Item1 + " - 2: " + spawnLane.Item2);
            Debug.Log("spawnlane 3: " + spawnLane.Item3 + " - 4: " + spawnLane.Item4);
            if (spawnLane.Item4 == 0)
            {
                GameObject _person = ObjectPool.SharedInstance.GetPooledObject(peopleTags[spawnIndex]);
                if (_person != null)
                {
                    Debug.Log("Person is not null");
                    _person.transform.position = new Vector3(spawnLane.Item1, northYStartCoord,0);
                    _person.SetActive(true);
                }
            }
            //Instantiate(peopleTags[spawnIndex], transform.position + new Vector3(spawnLane.Item1, northYStartCoord, 0), transform.rotation);
            else
            //Instantiate(carTags[spawnIndex], transform.position + new Vector3(spawnLane.Item1, northYStartCoord, 0), transform.rotation);
            {
                GameObject _car = ObjectPool.SharedInstance.GetPooledObject(carTags[spawnIndex]);
                if (_car != null)
                {
                    _car.transform.position = new Vector2(spawnLane.Item1, northYStartCoord);
                    _car.SetActive(true);
                }
            }
            spawnTime = GetSpawnTime();

            yield return new WaitForSeconds(spawnTime);
        } while (isLooping);

    }

    private (float, float, int, int) GetSpawnLane()
    {
        int randX = Random.Range(1, 16);

        // People Left Lane
        if (randX <= 5)
        {
            return (peopleLeftLaneX, northYStartCoord, peopleTags.Count, 0);
        }
        // People Right Lane
        if (randX <= 10)
        {
            return (peopleRightLaneX, northYStartCoord, peopleTags.Count, 0);
        }
        // Car
        return (carX, northYStartCoord, carTags.Count, 1);
    }


    private float GetSpawnTime()
    {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }    
}
