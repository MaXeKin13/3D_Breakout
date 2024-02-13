using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //follow ball variables
    public Transform player;
    private Transform _activeBullet;
    public int _activeBulletIndex;


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
            // transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
            transform.position = targetPosition;
            // Look at the bullet
            transform.LookAt(_activeBullet);
            
            yield return null;
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
                _activeBulletIndex = 0;
                transform.parent = player;
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;
            }

            
        }
    }
}
