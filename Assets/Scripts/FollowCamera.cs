using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //follow ball variables
    private Transform _activeBullet;
    private int _activeBulletIndex;


    private void Awake()
    {
        throw new NotImplementedException();
    }

    private IEnumerator FollowBall()
    {
        int prevIndex = _activeBulletIndex;
        _activeBullet = GameManager.Instance.activeBullets[_activeBulletIndex].transform;
        //transform.parent = _activeBullet;
        Debug.Log(_activeBullet);
        while (_activeBulletIndex == prevIndex && _activeBullet != null)
        {
            //take into account velocity of ball for camera angle
            Debug.Log("active");
            yield return null;
        }
        Debug.Log("endOfCouroutine");

        //transform.parent = GameManager.Instance.player.transform;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log(_activeBulletIndex+1);
            Debug.Log(GameManager.Instance.activeBullets.Count);
            if (_activeBulletIndex < GameManager.Instance.activeBullets.Count)
            {
                Debug.Log("right arrow");
                StartCoroutine(FollowBall());
                _activeBulletIndex++;
            }
            else
            {
                _activeBulletIndex = 0;
            }

            
        }
    }
}
