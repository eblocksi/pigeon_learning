using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Target SO", fileName = "New_Target")]
public class WaveConfigSO : ScriptableObject
{
    public int numberOfPrefabsToCreate;
    public Vector2[] spawnPoints;
    public GameObject[] prefabs;

    
    
}


/*
 * It looks like i use the scriptableobject to spawn the enemies
 * I get rid of all my prefabs, and create "Targets" instead
 * Then I can use the spawnenemy to pull a random target
 * and since all the values are preset on each card (xcor, movespeed, animation, etc..)
 * it'll just go to the right place and act correclty
 * */
