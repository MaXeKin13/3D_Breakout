using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    public static ScoreManager Instance;
    private int _score;

    private void Awake()
    {
        Instance = (Instance == null) ? this : Instance;
    }

    public void AddScore(int num)
    {
        _score += num;
        scoreText.text = "Score: " + _score.ToString();
    }
}
