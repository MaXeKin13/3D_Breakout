using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public GameObject Roof;
    public Vector3 up;
    public Vector3 right;
    public Vector3 down;
    public Vector3 left;

    private PlayerInputACtions _playerInputACtions;
    private PlayerInput _playerInput;

    public Transform snapCam;

    public Vector2 inputVector;

    private GameObject _mainCamera;


    //roof variables
    private MeshRenderer[] _roofRenderers;
    private SpriteRenderer[] _roofSprites;
    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInputACtions = new PlayerInputACtions();
        _playerInputACtions.CameraMap.Enable();

        _playerInputACtions.CameraMap.CamControl.performed += Snap;
        _playerInputACtions.CameraMap.CamControl.performed += ChangeToSecondCam;
        _playerInputACtions.CameraMap.CamControl.canceled += ChangeToMainCam;


        _mainCamera = Camera.main.gameObject;
        //_playerInputACtions.CameraMap.SnapCamera.performed += ctx => ChangeView(ctx.ReadValue<float>());

        //set up roof Variables
        _roofRenderers = Roof.transform.GetComponentsInChildren<MeshRenderer>();
        _roofSprites = Roof.transform.GetComponentsInChildren<SpriteRenderer>();
    }

    private void Snap(InputAction.CallbackContext context)
    {
         inputVector = _playerInputACtions.CameraMap.CamControl.ReadValue<Vector2>();
         ChangeView(inputVector);
    }
    
    public void ChangeView(Vector2 view)
    {
        if (view == Vector2.up)
        {
            snapCam.position = up;
            snapCam.rotation = Quaternion.Euler(90, 0, 0);

            foreach (MeshRenderer mesh in _roofRenderers)
            {
                mesh.enabled = false;
                
                
            }
            foreach (SpriteRenderer sprite in _roofSprites)
                sprite.enabled = false;
        }
        /*if (view == Vector2.right)
        {
            snapCam.position = right;
            snapCam.rotation = Quaternion.Euler(0, -90, 0);
        }*/
        if (view == Vector2.down)
        {
            snapCam.position = down;
            snapCam.rotation = Quaternion.Euler(-90, 0, 0);
        }
        /*if (view == Vector2.left)
        {
            snapCam.position = left;
            snapCam.rotation = Quaternion.Euler(0, 90, 0);
        }*/
    }
    public void ChangeToSecondCam(InputAction.CallbackContext context)
    {
        
        snapCam.gameObject.SetActive(true);
            //_mainCamera.SetActive(false);
        
    }

    private void ChangeToMainCam(InputAction.CallbackContext context)
    {
        // = Roof.transform.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in _roofRenderers)
            mesh.enabled = true;
        foreach (SpriteRenderer sprite in _roofSprites)
            sprite.enabled = true;
        snapCam.gameObject.SetActive(false);
        //_mainCamera.SetActive(true);
        
    }

    
   
}
