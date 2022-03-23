using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Stage Completition")]
    [SerializeField] Slider stageSlider;
    private float startTime;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        stageSlider.maxValue = 1f;
        startTime = Time.time;
    }

    private void Update()
    {
        stageSlider.value = (Time.time - startTime) / 30f;
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }
}
