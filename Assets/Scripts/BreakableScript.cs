using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour
{
    public bool canBeDestroyed = true;
    public bool canHighlight;
    public int health;
    private Material _mat;
    [HideInInspector] public MeshRenderer highlight;
    [HideInInspector] public Vector3 xNormal;
    private void Start()
    {
        xNormal = transform.right;
        _mat = GetComponent<MeshRenderer>().material;
        ChangeColor();
        highlight = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Bullet") && canBeDestroyed)
           GetHit();
    }

    void GetHit()
    {
        health--;
        ChangeColor();
        ScoreManager.Instance.AddScore(1);
        if(health <= 0)
            Death();
            
    }

    public void SetHighlight(bool active)
    {
        if (active)
            highlight.enabled = true;
        else
            highlight.enabled = false;

    }

    void ChangeColor()
    {
        switch (health)
        {
            case 4:
                _mat.color = Color.yellow;
                break;
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
