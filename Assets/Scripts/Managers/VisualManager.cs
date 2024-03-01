using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager: MonoBehaviour
{
    public static VisualManager Instance;

    [Space]
    public GameObject trajectory;
    public GameObject cursor;
    [Space]
    
    public Material[] blockMaterials;
    public GameObject blockDestroySystem;

    [Space]
    public GameObject GameplayUI;
    public GameObject WinUI;


   
    [SerializeField] public List<DiscoUI> discoUI = new List<DiscoUI>();

    void Awake()
    {
        Instance = (Instance == null) ? this : Instance;
    }

   public void EndGameUI()
    {
        GameplayUI.SetActive(false);
        WinUI.SetActive(true);
    }

    [Serializable] 
    public struct DiscoUI
    {
        public Sprite[] discos;        
    }
}
