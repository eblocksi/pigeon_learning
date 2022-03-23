using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargets : MonoBehaviour
{
    [Header("People")]
    [SerializeField] List<GameObject> people;

    [Header("Cars")]
    [SerializeField] List<GameObject> cars;

    [Header("General")]
    public float minSpawnTime = .5f;
    public float maxSpawnTime = 3f;
    public float peopleLeftLaneX = -6.75f;
    public float peopleRightLaneX = 6.75f;
    public float carX = 0f;

    private float spawnTime;
    private bool isCar;
    
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
                Instantiate(people[spawnIndex], transform.position + new Vector3(spawnLane.Item1, spawnLane.Item2, 0), transform.rotation);
            else
                Instantiate(cars[spawnIndex], transform.position + new Vector3(spawnLane.Item1, spawnLane.Item2, 0), transform.rotation);

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
            isCar = false;
            return (peopleLeftLaneX, northYStartCoord, people.Count, 0);
        }
        // People Right Lane
        if (randX <= 10)
        {
            isCar = false;
            return (peopleRightLaneX, northYStartCoord, people.Count, 0);
        }
        // Car
        isCar = true;
        return (carX, northYStartCoord, cars.Count, 1);
    }


    private float GetSpawnTime()
    {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }    


    public bool IsCar()
    {
        return isCar;
    }
}
