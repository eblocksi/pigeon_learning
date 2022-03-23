using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int currentScore;

    static ScoreKeeper instance;

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        currentScore = 0;
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void ModifyScore(int points)
    {
        currentScore += points;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

}
