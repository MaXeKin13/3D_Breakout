using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public Vector3 up;
    public Vector3 right;
    public Vector3 down;
    public Vector3 left;

    private PlayerInputACtions _playerInputACtions;
    private PlayerInput _playerInput;

    public Transform snapCam;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInputACtions = new PlayerInputACtions();
        _playerInputACtions.CameraMap.Enable();

        _playerInputACtions.CameraMap.SnapCamera.performed += Snap;
        //_playerInputACtions.CameraMap.SnapCamera.performed += ctx => ChangeView(ctx.ReadValue<float>());

    }

    private void Update()
    {
        
    }

    private void Snap(InputAction.CallbackContext context)
    {
        
    }
    public void ChangeView(int view)
    {
        switch (view)
        {
            case 0:
                snapCam.position = up;
                snapCam.rotation = Quaternion.Euler(90,0, 0);
                break;
            case 1:
                snapCam.position = right;
                snapCam.rotation = Quaternion.Euler(0,-90, 0);
                break;
            case 2:
                snapCam.position = down;
                snapCam.rotation = Quaternion.Euler(-90,0, 0);
                break;
            case 3:
                snapCam.position = left;
                snapCam.rotation = Quaternion.Euler(0,90, 0);
                break;
        }
    }
}
