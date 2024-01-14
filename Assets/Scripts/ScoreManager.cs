using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    public ScoreManager Instance;
    private int _score;
    public int Score{get; set;}

    private void Awake()
    {
        Instance = (Instance == null) ? this : Instance;
    }
}
