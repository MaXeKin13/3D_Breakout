using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryScript : MonoBehaviour
{
   private LineRenderer _lineRenderer;
    private Transform _previousHit;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        SendRaycast();
    }

    void SendRaycast()
    {
        // Send raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, 1 << 8))
        {
            // Get exact point of contact
            var contactPoint = hit.point;

            // Calculate trajectory
            Vector3 reflectedDirection = Vector3.Reflect(transform.forward, hit.normal);

            // Send raycast towards reflected direction
            Vector3 reflectedContactPoint = contactPoint + reflectedDirection * 5f;

            // CalculateTrajectory(contactPoint, reflectedContactPoint);
            Raycast2(contactPoint, reflectedContactPoint);
            SetLinePoints(contactPoint, reflectedContactPoint);

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
        }
    }

    void Raycast2(Vector3 pos, Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(pos, dir, out hit, 1 << 8))
        {
            // Get exact point of contact
            var contactPoint = hit.point;

            // Activate outline
            hit.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        }
    }

    // Set Line Renderer points along raycast.
    private void SetLinePoints(Vector3 contactPoint, Vector3 secondPoint)
    {
        _lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z));
        _lineRenderer.SetPosition(1, contactPoint);
        _lineRenderer.SetPosition(2, secondPoint);
    }
}