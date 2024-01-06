using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    [Space] 
    public float power;
    
    private Rigidbody _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Shoot();
    }

    private void Shoot()
    {
        _rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Breakable"))
        {
            BreakableScript obj = other.transform.GetComponent<BreakableScript>();
        }
    }
}
