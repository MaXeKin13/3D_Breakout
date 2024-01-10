using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryScript : MonoBehaviour
{
   private LineRenderer _lineRenderer;
    private Transform _previousHit;
    private Transform _previousHit2;

    //To Do: array for hits (instead of just 2 objects)

    private List<Vector3> _hits;
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _hits = new List<Vector3>();
    }

    private void FixedUpdate()
    {
        SendRaycast();
    }

    void SendRaycast()
    {
        // Send initial raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, 1 << 8))
        {
            // Get exact point of contact
            var contactPoint = hit.point;

            // Calculate trajectory
            Vector3 reflectedDirection = Vector3.Reflect(transform.forward, hit.normal);

            // Send raycast towards reflected direction
            Vector3 reflectedContactPoint = contactPoint + reflectedDirection * 100f; //length of 5f;
            
            //if second raycast doesnt hit another object
            if (!Raycast2(contactPoint, reflectedDirection))
            {
                SetLinePoints(reflectedContactPoint, 2);
            }
            //set second line position to first contact point
            SetLinePoints(contactPoint, 1);
            
            // Activate outline
            hit.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;

            // Check if the current hit object is different from the previous one
            if (_previousHit != null && _previousHit != hit.transform)
            {
                // Disable sprite renderer of the previous hit object
                _previousHit.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            }
            // Set the current hit object as the previous hit
            _previousHit = hit.transform;
        }
        else
        {
            // If nothing is hit, disable the sprite renderer of the previous hit object
            if (_previousHit != null)
                _previousHit.GetChild(0).GetComponent<MeshRenderer>().enabled = false;

            _previousHit = null;
            
            SetLinePoints(transform.position + transform.forward * 5f, 1);
            SetLinePoints(transform.position + transform.forward * 5f, 2);
        }
    }

    bool Raycast2(Vector3 pos, Vector3 dir)
    {
        Debug.DrawRay(pos, dir, Color.blue, 1f);
        RaycastHit hit;
        if (Physics.Raycast(pos, dir, out hit, Mathf.Infinity, 1<<8))
        {
            
            // Get exact point of contact
            var contactPoint = hit.point;
            Debug.Log(hit);
            
            // Activate outline
            if(hit.transform.CompareTag("Breakable"))
                hit.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            
            if (_previousHit2 != null && _previousHit2 != hit.transform)
            {
                // Disable sprite renderer of the previous hit object
                _previousHit2.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            }

            // Set the current hit object as the previous hit
            _previousHit2 = hit.transform;
            //set line
            SetLinePoints(contactPoint, 2);
            return true;
        }
        else
        {
            return false;
        }
    }

    // Set Line Renderer points along raycast.
    private void SetLinePoints(Vector3 contactPoint, int index)
    {
        _lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z));
        _lineRenderer.SetPosition(index, contactPoint);
        
    }
    
   
}
