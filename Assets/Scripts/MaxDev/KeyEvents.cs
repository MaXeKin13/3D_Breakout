using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvents : MonoBehaviour
{
    public delegate void OnSpacePress();
    public static event OnSpacePress SpacePress;

    private void Start()
    {
        //Subscribe test function to SpacePress;
        SpacePress += test;
    }

    private void Update()
    {
        //Invoke the SpacePress Event (and all functions subscribed to it)
        if(Input.GetKeyDown(KeyCode.Space))
           SpacePress?.Invoke();
    }

    void test()
    {
        Debug.Log("test");
    }

    
}
