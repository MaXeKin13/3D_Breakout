using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager: MonoBehaviour
{
    public static VisualManager Instance;

    public GameObject blockDestroySystem;
    void Start()
    {
        Instance = (Instance == null) ? this : Instance;
    }

    
}
