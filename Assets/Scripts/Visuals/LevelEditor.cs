using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public GameObject block;

    //grid constrained to certain area (have to set bounds), instead snap to certain position.
    //grid only useful if you need to know neighbors, etc;
    //use local position for everything + parent it

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
            grid = new GameObject[rowLength, collumnLength];
        }
        //get size of block
        cellSize = block.GetComponentInChildren<MeshRenderer>().bounds.size;

        //set bounds
        int boundsX = Mathf.RoundToInt(cellSize.x);
        int boundsY = Mathf.RoundToInt(cellSize.y);

        for (int y = 0; y< collumnLength; y++)
        {
            //spawn collumn first
            for (int x = 0; x < rowLength; x++)
            {                            
                grid[x, y] = Instantiate(block, new Vector3(transform.position.x, transform.position.y, 0f) + new Vector3(x * boundsX, y * boundsY, transform.position.z),
                    Quaternion.identity, transform);                
            }
        }
    }

    public void RemoveGrid()
    {
        foreach (GameObject go in grid)
        {
            DestroyImmediate(go);
        }
        grid = null;
    }
}
