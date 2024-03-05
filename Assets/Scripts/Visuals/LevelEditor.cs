using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public GameObject block;

    //grid constrained to certain area (have to set bounds), instead snap to certain position.
    //grid only useful if you need to know neighbors, etc;
    //use local position for everything + parent it
    public Vector3 position;
    public Vector3 rotation;

    public int rowLength;
    public int collumnLength;

    //size of each grid
    public Vector3 cellSize;


    //2d array for grid data (center positions)
    public GameObject[,] grid;
    //grid position can be different from grid value;

    public void SetGrid()
    {
        if(grid == null)
        {
            grid = new GameObject[20, collumnLength];
        }
        //get size of block
        cellSize = block.GetComponentInChildren<MeshRenderer>().bounds.size;
        Debug.Log("Spawn");
        //set bounds
        int currentY = Mathf.RoundToInt(position.y);

        for(int i = 0; i< rowLength; i++)
        {
            int boundsX = Mathf.RoundToInt(cellSize.x);
            Debug.Log(boundsX);
            grid[i, currentY] = Instantiate(block, new Vector3((boundsX + i)* boundsX, currentY, position.z), Quaternion.identity, transform);
            //if done through y, swap i and currentY
        }
    }
}
