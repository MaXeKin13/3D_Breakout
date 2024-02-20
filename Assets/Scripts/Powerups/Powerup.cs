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
        Big,
    }

    //object to add to bullet when hit
    public GameObject bulletEffect;
    public GameObject particleSystem;
    public GameObject hitEffect;
     public PowerupType _powerupType = new PowerupType();

     public void OnActivate()
     {
         GetComponent<Collider>().enabled = false;
         GetComponent<MeshRenderer>().enabled = false;
         transform.GetChild(0).gameObject.SetActive(false);
         GameObject _system = Instantiate(particleSystem, transform);
         Destroy(gameObject, 2f);
         
         
     }
}
