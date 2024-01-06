using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour
{
    public int health;
    private Material _mat;

    private void Start()
    {
        _mat = GetComponent<MeshRenderer>().material;
        ChangeColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Bullet"))
           GetHit();
    }

    void GetHit()
    {
        health--;
        ChangeColor();
        if(health <= 0)
            Death();
            
    }

    void ChangeColor()
    {
        switch (health)
        {
            case 3: 
                _mat.color = Color.green;
                break;
            case 2:
                _mat.color = Color.blue;
                break;
            case 1:
                _mat.color = Color.red;
                break;
        }
    }
    void Death()
    {
        
        Destroy(gameObject);
    }
    
    
}