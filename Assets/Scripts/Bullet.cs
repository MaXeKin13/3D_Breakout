using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool hardTimer;
    public float speed;
    public float hitTimer = 5f;
    public GameObject hitEffect;
    public GameObject deathEffect;
    [Space] 
    public float power;

    private Rigidbody _rb;
    private float currentTime;

    //GravField
    private bool _inGrav;

    private AudioSource _audioSource;



    private void Awake()
    {
        GameManager.Instance.AddBullet(this);
        _rb = GetComponent<Rigidbody>();
        Shoot();
        StartCoroutine(DespawnTimer());

        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = VisualManager.Instance.shootSound;
        _audioSource.Play();
    }


    private void Shoot()
    {
        _rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Breakable"))
        {
            if (_audioSource.clip == VisualManager.Instance.shootSound)
                _audioSource.clip = VisualManager.Instance.hitSound;
            PlayAudio();

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
            //change hit effect
            hitEffect = _powerup.hitEffect;
            //remove visual effect
            Destroy(transform.GetChild(0).gameObject);
            //add visual effect
            Instantiate(_powerup.bulletEffect, transform);
            
            _powerup.OnActivate();
            if (_powerup._powerupType == Powerup.PowerupType.Double)
            {
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

        if(other.transform.CompareTag("Respawn"))
        {
            if (GameManager.Instance.activeBullets.Contains(this))
            {
                GameManager.Instance.RemoveBullet(this);

                Destroy(gameObject);
            }
        }

        if(other.transform.CompareTag("Grav"))
        {
            _inGrav = true;
            StartCoroutine(GravForce(other.transform, other.transform.GetComponent<GravSphere>().strength));
        }
    }

    private IEnumerator DespawnTimer()
    {
        if (!hardTimer)
        {
            while (currentTime < hitTimer)
            {
                currentTime += Time.deltaTime;
                //add visuals to show delay
                yield return null;
            }
            if (GameManager.Instance.activeBullets.Contains(this))
            {
                GameManager.Instance.RemoveBullet(this);
                
                Debug.Log("help");
                //Destroy(gameObject);
            }
        }
        else
        {
            yield return new WaitForSeconds(GameManager.Instance.destroyDelay);
            if (GameManager.Instance.activeBullets.Contains(this))
            {
                GameManager.Instance.RemoveBullet(this);
                StopBullet();
                Destroy(gameObject, 1f);
            }
        }
        
    }

    private void StopBullet()
    {
        _rb.velocity = Vector3.zero;
        GetComponent<MeshRenderer>().enabled = false;
        GameObject deathParticle = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathParticle, 1f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Grav"))
            _inGrav = false;
    }

    private IEnumerator GravForce(Transform gravSphere, float strength)
    {
        while (_inGrav) 
        {
            _rb.AddForce(gravSphere.position - transform.position* strength, ForceMode.Force );
            yield return new WaitForFixedUpdate();
        }
    }

    
    private void PlayAudio()
    {
        _audioSource.Stop();

        _audioSource.Play();
    }
    
}
    

