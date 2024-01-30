using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float hitTimer = 5f;
    public GameObject hitEffect;
    [Space] 
    public float power;
    
    private Rigidbody _rb;
    private float currentTime;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Shoot();
        StartCoroutine(DespawnTimer());
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

            currentTime = 0;
            
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

    private IEnumerator DespawnTimer()
    {
        while (currentTime < hitTimer)
        {
            currentTime += Time.deltaTime;
            
            yield return null;
        }
        Destroy(gameObject);
    }
    
}
