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
    
    void Awake()
    {
        Instance = (Instance == null) ? this : Instance;
    }

   
}
