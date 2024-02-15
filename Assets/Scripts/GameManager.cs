using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameObject player;
    public GameObject bullet;
    public int ammo;
    [Space]
    public float destroyDelay = 2f;

    public List<Bullet> activeBullets;
    
    private void Awake()
    {
        Instance = (Instance == null) ? this : Instance;

        activeBullets = new List<Bullet>();
        LockCursor();
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Alpha0))
            LockCursor();*/
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}
