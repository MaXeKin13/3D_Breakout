using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    public GameObject hitEffect;
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
            GameObject hit = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(hit, 0.5f);
        }

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Powerup"))
        {
            if (other.transform.GetComponent<Powerup>()._powerupType == Powerup.PowerupType.Double)
            {
                other.gameObject.SetActive(false);
                _rb.AddForce(transform.right * speed / 2f, ForceMode.Impulse);
                Instantiate(gameObject);
            }
        }
    }
}
