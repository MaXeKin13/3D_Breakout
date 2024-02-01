using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTrajectory : MonoBehaviour
{
    //keep track of hit
    public int maxHits;
    public Vector3[] hitPoints;


    private int _hitIndex;
    
    
    private void SendRaycast(Vector3 initialPos, Vector3 dir)
    {
        if (Physics.Raycast(initialPos, dir, out RaycastHit hit, Mathf.Infinity, 1 << 8))
        {
            // Get exact point of contact
            Vector3 contactPoint = hit.point;

            // Calculate trajectory
            Vector3 reflectedDirection = Vector3.Reflect(transform.forward, hit.normal);

            // Send raycast towards reflected direction
            Vector3 reflectedContactPoint = contactPoint + reflectedDirection * 100f; //length of 5f;

            if (_hitIndex < maxHits)
            {
                SendRaycast(contactPoint, reflectedDirection);
                _hitIndex++;
                return;
                
            }

            

        }
    }
    
    
    
    
}
