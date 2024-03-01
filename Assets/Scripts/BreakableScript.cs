using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour
{
    public bool canBeDestroyed = true;
    public bool canHighlight;
    public bool isTransparent;
    [Space]
    public int health;
    
    [HideInInspector] public MeshRenderer highlight;
    [HideInInspector] public Vector3 xNormal;

    private MeshRenderer _mat;
    public SpriteAnim _spriteAnim;
    private void Start()
    {
        xNormal = transform.right;
        _mat = GetComponent<MeshRenderer>();

        _spriteAnim = GetComponentInChildren<SpriteAnim>();
        //ChangeColor();
        highlight = transform.GetChild(0).GetComponent<MeshRenderer>();
        ChangeMaterial();

        ChangeDisco();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Bullet") && canBeDestroyed)
           GetHit();
    }

    void GetHit()
    {
        health--;
        ChangeMaterial();
        ChangeDisco();
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

    void ChangeMaterial()
    {
        if (!isTransparent)
            switch (health)
            {
                case 4:
                    _mat.material = VisualManager.Instance.blockMaterials[3];
                    break;
                case 3:
                    _mat.material = VisualManager.Instance.blockMaterials[2];
                    break;
                case 2:
                    _mat.material = VisualManager.Instance.blockMaterials[1];
                    break;
                case 1:
                    _mat.material = VisualManager.Instance.blockMaterials[0];
                    break;
            }
    }

    void ChangeDisco()
    {
       
            switch (health)
            {
                case 4:
                    //Get List of DiscoUI class, then get the array of sprites in discoUI[4]
                    _spriteAnim.sprites = VisualManager.Instance.discoUI[3].discos;
                    break;
                case 3:
                    _spriteAnim.sprites = VisualManager.Instance.discoUI[2].discos;
                    break;
                case 2:
                    _spriteAnim.sprites = VisualManager.Instance.discoUI[1].discos;
                    break;
                case 1:
                    _spriteAnim.sprites = VisualManager.Instance.discoUI[0].discos;
                    break;
            }
    }
    void ChangeColor()
    {
        /*if(!isTransparent)
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
        else
            switch (health)
            {
                case 4:
                    _mat.color = new Color(1, 0.92f, 0.016f, 0.3f);
                    break;
                case 3: 
                    _mat.color = new Color(0, 1f, 0f, 0.3f);
                    break;
                case 2:
                    _mat.color = new Color(0, 0, 1, 0.3f);
                    break;
                case 1:
                    _mat.color = new Color(1, 0, 0f, 0.3f);
                    break;
            }
        */
    }
    void Death()
    {
        GameObject system = Instantiate(VisualManager.Instance.blockDestroySystem, transform.position, Quaternion.identity);
        Destroy(system, 1f);
        Destroy(gameObject);
    }
    
    
    
}
