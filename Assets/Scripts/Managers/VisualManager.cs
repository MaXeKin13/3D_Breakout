using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualManager: MonoBehaviour
{
    public static VisualManager Instance;

    [Space]
    public GameObject trajectory;
    public GameObject cursor;
    [Space]
    
    public Material[] blockMaterials;
    public GameObject blockDestroySystem;
    public GameObject blockHitSystem;

    [Space]
    public GameObject GameplayUI;
    public GameObject WinUI;


   
    [SerializeField] public List<DiscoUI> discoUI = new List<DiscoUI>();
    [Space]
    public AudioClip shootSound;
    public AudioClip hitSound;


    private GameObject player;

    

    void Awake()
    {
        Instance = (Instance == null) ? this : Instance;

        player = GameObject.Find("player");

        trajectory = player.transform.GetComponentInChildren<NewTrajectory>().gameObject;
        GameObject canvas = GameObject.Find("Canvas");
        WinUI = canvas.transform.GetChild(0).gameObject;
        WinUI.SetActive(false);
        GameplayUI = canvas.transform.GetChild(1).gameObject;
        cursor = GameplayUI.transform.GetChild(0).gameObject;

    }

   public void EndGameUI()
    {
        GameplayUI.SetActive(false);
        WinUI.SetActive(true);
        WinUI.transform.GetChild(3).GetComponent<Text>().text = ScoreManager.Instance._score.ToString();
    }

    [Serializable] 
    public struct DiscoUI
    {
        public Sprite[] discos;        
    }
}
