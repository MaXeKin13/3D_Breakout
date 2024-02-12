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
        GameManager.Instance.activeBullets.Add(this);
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
            
            Powerup _powerup = other.GetComponent<Powerup>();
            //remove visual effect
            Destroy(transform.GetChild(0).gameObject);
            //add visual effect
            Instantiate(_powerup.bulletEffect, transform);
            
            _powerup.OnActivate();
            if (_powerup._powerupType == Powerup.PowerupType.Double)
            {
                //other.gameObject.SetActive(false);
                _rb.AddForce(transform.right * speed / 2f, ForceMode.Impulse);
                GameObject bull2 = Instantiate(gameObject);
                
                bull2.GetComponent<Rigidbody>().AddForce(-bull2.transform.right * speed / 2f, ForceMode.Impulse);
                //destroy original effect
                Destroy(bull2.transform.GetChild(0).gameObject);
            }
            if (_powerup._powerupType == Powerup.PowerupType.Triple)
            {
                GameObject bull2 = Instantiate(gameObject);
                //destroy original effect
                Destroy(bull2.transform.GetChild(0).gameObject);
                
                _rb.AddForce(transform.right * speed / 2f, ForceMode.Impulse);
                GameObject bull3 = Instantiate(gameObject);
                bull3.GetComponent<Rigidbody>().AddForce(-bull3.transform.right * speed / 2f, ForceMode.Impulse);
                //destroy original effect
                Destroy(bull3.transform.GetChild(0).gameObject);
            }
        }
    }

    private IEnumerator DespawnTimer()
    {
        while (currentTime < hitTimer)
        {
            currentTime += Time.deltaTime;
            //add visuals to show delay
            yield return null;
        }
        
        GameManager.Instance.activeBullets.Remove(this);
        Destroy(gameObject);
    }
    
}
