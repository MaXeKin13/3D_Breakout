using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public bool hardFollow;
    [Space]
    //follow ball variables
    public Transform player;
    
    public int _activeBulletIndex;
    [Space]
    public float lerpAmount = 1f;
    public Vector3 offset;

    private Transform _activeBullet;

    private void Awake()
    {
        
    }

    private IEnumerator FollowBall()
    {
        int prevIndex = _activeBulletIndex;
        _activeBullet = GameManager.Instance.activeBullets[_activeBulletIndex-1].transform;
        transform.parent = null;
        Debug.Log(_activeBulletIndex);
        while (_activeBulletIndex == prevIndex && _activeBullet != null)
        {
            var velocity = _activeBullet.GetComponent<Rigidbody>().velocity.normalized;

            // Calculate the target position
            Vector3 targetPosition = _activeBullet.position - velocity;

            // Gradually move the camera towards the target position
            if(!hardFollow)
                transform.position = Vector3.Lerp(transform.position, targetPosition + offset, Time.deltaTime * lerpAmount);
            else
                transform.position = targetPosition;
            // Look at the bullet
            transform.LookAt(_activeBullet);
            
            yield return null;

            if (_activeBullet == null)
                ResetCamera();
        }
        

       
        Debug.Log(prevIndex);

        //transform.parent = GameManager.Instance.player.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log(_activeBulletIndex);
            Debug.Log(GameManager.Instance.activeBullets.Count);
            if (_activeBulletIndex < GameManager.Instance.activeBullets.Count)
            {
                _activeBulletIndex++;
                StartCoroutine(FollowBall());
                
            }
            else
            {
                ResetCamera();
            }

            
        }
    }

    public void ResetCamera()
    {
        _activeBulletIndex = 0;
        transform.parent = player;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}
