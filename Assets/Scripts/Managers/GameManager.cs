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

    public void AddBullet(Bullet bullet)
    {
        activeBullets.Add(bullet);
    }
    public void RemoveBullet(Bullet bullet)
    {
        activeBullets.Remove(bullet);
        CheckEndGame();
    }
    private void CheckEndGame()
    {
        Debug.Log("CheckEnd");
        if(ammo == 0 && activeBullets.Count == 0)
        {
            StartCoroutine(FinishLevel());
        }
    }

    private IEnumerator FinishLevel()
    {
        Debug.Log("EndOfGame");
        yield return new WaitForSeconds(1f);
        VisualManager.Instance.EndGameUI();
    }
}
