using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryScript : MonoBehaviour
{
   private LineRenderer _lineRenderer;
    public Transform _previousHit;
    public Transform _nextHit;
    
    //To Do: array for hits (instead of just 2 objects)

    private List<Vector3> _hits = new();
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        
    }

    private void Update()
    {
        SendRaycast();
    }

    private void SendRaycast()
    {
        // Send initial raycast
        
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, 1 << 8))
        {
            // Get exact point of contact
            Vector3 contactPoint = hit.point;

            // Calculate trajectory
            Vector3 reflectedDirection = Vector3.Reflect(transform.forward, hit.normal);

            // Send raycast towards reflected direction
            Vector3 reflectedContactPoint = contactPoint + reflectedDirection * 100f; //length of 5f;
            
            //if second raycast doesnt hit another object
            if (!SendNextRaycast(contactPoint, reflectedDirection))
            {
                SetLinePoints(reflectedContactPoint, 2);
            }
            //set second line position to first contact point
            SetLinePoints(contactPoint, 1);
            
            // Activate outline
            //hit.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            hit.transform.GetComponent<BreakableScript>().SetHighlight(true);
            // Check if the current hit object is different from the previous one
            if (_previousHit != null && _previousHit != hit.transform)
            {
                // Disable sprite renderer of the previous hit object
                //_previousHit.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                _previousHit.GetComponent<BreakableScript>().SetHighlight(false);
            }
            // Set the current hit object as the previous hit
            _previousHit = hit.transform;
        }
        else
        {
            // If nothing is hit, disable the sprite renderer of the previous hit object
            if (_previousHit != null)
                _previousHit.GetComponent<BreakableScript>().SetHighlight(false);

            _previousHit = null;
            Vector3 contactPoint = transform.position + transform.forward;
            SetLinePoints(transform.position + transform.forward * 5f, 1);
            SetLinePoints(transform.position + transform.forward * 5f, 2);
        }
    }

    //I think you could come up with a generalized method to cast the ray as you need it.
    //Once you get the list going, you could safe on having this entire method cluttering your code.
    bool SendNextRaycast(Vector3 pos, Vector3 dir)
    {
        Debug.DrawRay(pos, dir, Color.blue, 1f);
        
        if (Physics.Raycast(pos, dir, out RaycastHit hit, Mathf.Infinity, 1<<8))
        {
            Debug.Log(hit);
            
            // Activate outline
            if(hit.transform.CompareTag("Breakable"))
                hit.transform.GetComponent<BreakableScript>().SetHighlight(true);
            
            if (_nextHit != null && _nextHit != hit.transform)
            {
                // Disable sprite renderer of the previous hit object
                if(_nextHit.transform.GetComponent<BreakableScript>())
                    _nextHit.transform.GetComponent<BreakableScript>().SetHighlight(false);
            }

            // Set the current hit object as the previous hit
            _nextHit = hit.transform;
            //set line
            SetLinePoints(hit.point, 2);
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
    
    //new system

    private void NewRaycast()
    {
        
    }
    //Tuple, output is point and hitTrans, input is pos and dir
    private (Vector3 point, Transform hitTrans) GetRaycastPoint(Vector3 pos, Vector3 dir)
    {
        if (Physics.Raycast(pos, dir, out RaycastHit hit, Mathf.Infinity, 1 << 8))
        {
            return (hit.point, hit.transform);
        }
        else
        {
            return (transform.position + transform.forward * 5f, null);
        }
    }
   
}
