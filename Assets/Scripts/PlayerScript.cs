using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private PlayerInputACtions _playerInputACtions;
    private PlayerInput _playerInput;
    
    
    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();

        //PlayerInputACtions playerInputACtions = new PlayerInputACtions();
        _playerInputACtions = new PlayerInputACtions();
        _playerInputACtions.Player.Enable();

        _playerInputACtions.Player.Shoot.performed += ShootBullet;
    }

    private void ShootBullet(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.ammo > 0)
        {
            GameObject bullet = Instantiate(GameManager.Instance.bullet, transform.position, transform.rotation);
            Destroy(bullet, GameManager.Instance.destroyDelay);
            GameManager.Instance.ammo--;
            ScoreManager.Instance.UpdateAmmo();
        }
    }
   
}
