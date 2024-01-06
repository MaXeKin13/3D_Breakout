using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameObject bullet;
    public float destroyDelay = 2f;

    private void Awake()
    {
        Instance = (Instance == null) ? this : Instance;
    }
}
