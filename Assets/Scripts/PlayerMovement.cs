using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
   
    public Transform _controlledObj;

    public float baseRotationSpeed = 2f;
    public float maxRotationSpeed = 10f;

    public float xRotateReduction= 1f;
    public float yRotateReduction= 1f;
    
    

    private Rigidbody _rb;
    private PlayerInput _playerInput;
    private PlayerInputACtions _playerInputACtions;
    
    private Transform _playerTrans;
    private Transform _cameraTrans;
    //rotate
    private Vector2 _rotateInput;
    //previous rotation value
    private Vector2 _previousRotateInput;

    private bool _isGrounded;
    private bool _isControllingCam;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();

        _playerTrans = GetComponent<Transform>();
        _controlledObj = GetComponent<Transform>();
        _cameraTrans = Camera.main.GetComponent<Transform>();

        //PlayerInputACtions playerInputACtions = new PlayerInputACtions();
        _playerInputACtions = new PlayerInputACtions();
        _playerInputACtions.Player.Enable();
        _playerInputACtions.Player.Jump.performed += Jump;
        
        //when player moves mouse
        _playerInputACtions.Player.Rotate.performed += OnMouseMove;
        //when player stops moving mouse
        _playerInputACtions.Player.Rotate.canceled += OnMouseStop;
        //when player right mouse click
        _playerInputACtions.Player.SwapControl.performed += SwitchCam;
        

    }

    private void FixedUpdate()
    {
        Vector2 inputVector = _playerInputACtions.Player.Movement.ReadValue<Vector2>();
        float speed = 5f;
        //_rb.AddForce(new Vector3(inputVector.x, 0, inputVector.y)*speed, ForceMode.Force);
        
        // Get the forward direction in local space
        Vector3 forwardDirection = _controlledObj.forward;

        // Transform the input vector to world space using the object's forward direction
        Vector3 movement = _controlledObj.TransformDirection(new Vector3(inputVector.x, 0, inputVector.y)) * speed;

        // Apply force in the transformed local space
        _rb.AddForce(movement, ForceMode.Force);
    }

    private void LateUpdate()
    {
        float horizontalRotation = _rotateInput.x* CalculateRotationSpeed()/xRotateReduction;
        float verticalRotation = _rotateInput.y* CalculateRotationSpeed()/yRotateReduction;

        _controlledObj.localEulerAngles += new Vector3(-verticalRotation, horizontalRotation, 0);

        _previousRotateInput = _rotateInput;
    }

    private float _previousRotationSpeed;
    public float smoothingFactor = 10f; // Adjust the value based on your preference
    private float CalculateRotationSpeed()
    {
        
            // Calculate the speed based on the change in input
            float rotationSpeed = baseRotationSpeed + _rotateInput.magnitude * maxRotationSpeed;
    
            // Optionally, you can apply a smoothing factor to make the rotation more gradual
            rotationSpeed = Mathf.Lerp(_previousRotationSpeed, rotationSpeed, Time.deltaTime * smoothingFactor);
    
            _previousRotationSpeed = rotationSpeed;

            return rotationSpeed;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && _isGrounded == true && !_isControllingCam)
            _rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        if(_isControllingCam)
            _rb.AddForce(Vector3.up * 1f, ForceMode.Impulse);
    }
    
    //my own attempt
    private void OnMouseMove(InputAction.CallbackContext context)
    {
        _rotateInput = context.ReadValue<Vector2>();
    }
    //stop camera rotation
    private void OnMouseStop(InputAction.CallbackContext context)
    {
        _rotateInput = Vector2.zero;
    }

    //when pressing right mouse can move the camera independently from character
    //doesnt go back to main view automatically
    //when press right mouse it snaps back
    
    //switch control to camera
    void SwitchCam(InputAction.CallbackContext context)
    {
        if (_controlledObj == _playerTrans)
        {
            _isControllingCam = true;
           _controlledObj = _cameraTrans;
           _cameraTrans.parent = null;
        }
        else
        {
            _isControllingCam = false;
            _controlledObj = _playerTrans;
            ResetCam();
        }
       
        _rb = _controlledObj.GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.None;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void ResetCam()
    {
        _cameraTrans.parent = _playerTrans;
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _cameraTrans.position = _playerTrans.position;
        _cameraTrans.rotation = _playerTrans.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
