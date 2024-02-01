using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType
    {
        Double,
        Triple,
        Fast,
    }

    public GameObject particleSystem;

     public PowerupType _powerupType = new PowerupType();

     public void OnActivate()
     {
         GetComponent<Collider>().enabled = false;
         GetComponent<MeshRenderer>().enabled = false;
         GameObject _system = Instantiate(particleSystem, transform);
         Destroy(gameObject, 2f);
     }
}
