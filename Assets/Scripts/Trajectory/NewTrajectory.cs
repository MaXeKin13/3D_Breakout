using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTrajectory : MonoBehaviour
{
    //keep track of hit
    public int maxHits;

    private int _hitIndex;


    private struct HitData
    {
        public Vector3 hitPoint;
        public Transform hitObj;
    }
   private HitData[] _hitDatas;
    private int hitNum;


    private LineRenderer _lineRenderer;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = maxHits;
        _hitDatas = new HitData[maxHits];
    }

    private void Update()
    {
        NewRaycast();
    }

    private void NewRaycast()
    {
        GetRaycastPoint(transform.position, transform.forward);
        _hitDatas = new HitData[maxHits];
        
    }
    
    //Tuple, output is point and hitTrans, input is pos and dir
   // private (Vector3 point, Transform hitTrans) GetRaycastPoint(Vector3 pos, Vector3 dir)
    private void GetRaycastPoint(Vector3 pos, Vector3 dir)
    {
        
        if (Physics.Raycast(pos, dir, out RaycastHit hit, Mathf.Infinity, 1 << 8) && hitNum < maxHits)
        {
            hitNum++;
            
            Vector3 contactPoint = hit.point;
            Vector3 reflectedDirection = Vector3.Reflect(dir, hit.normal);
            
            HitData hitData = new HitData();
            hitData.hitPoint = hit.point;
            
            //check if hitObj has changed
            hitData.hitObj = hit.transform;
            
            _hitDatas[hitNum-1] = hitData;
            GetRaycastPoint(hit.point, reflectedDirection);
        }
        else
        {
            //Debug.Log("reset to 0");
            hitNum = 0; //change?
        }
        //set all Line Points from Hit Array
        SetAllLinePoints();
    }

    private void SetAllLinePoints()
    {
        _lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z));
        for (int i = 0; i < _hitDatas.Length; i++)
        {
            //set position of hits after player pos
            if(i+1 < _hitDatas.Length)
                _lineRenderer.SetPosition(i+1, _hitDatas[i].hitPoint);
        }
    }
}
