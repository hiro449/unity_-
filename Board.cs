using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Transform empty;

    [SerializeField]
    private int width = 10, height = 30, header = 8;

    
    private Transform[,] grid;

    private void Awake()
    {
        grid = new Transform[width, height];
    }


    private void Start()
    {
        CreateBoard();
    }

    void CreateBoard()
    {

        if (empty)
        {

            for (int y = 0; y < height - header; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Transform clone = Instantiate(empty, new Vector3(x,y,0), Quaternion.identity);

                    if (clone)
                    {
                        clone.transform.parent = transform;
                    }
                }
            }

        }

    }


    public bool CheckPosition(Block block)
    {
        foreach (Transform item in block.transform)
        {
            Vector2 pos = Rounding.Round(item.position);

            if(!CheckOutBoard((int)pos.x, (int)pos.y))
            {
                return false;
            }

            if (BlockCheck((int)pos.x, (int)pos.y, block))
            {
                return false;
            }
        }

        return true;
    }

    bool CheckOutBoard(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0);
    }

    bool BlockCheck(int x, int y, Block block)
    {
        return (grid[x,y] != null && grid[x,y].parent != block.transform);
    }

    public void SaveInGrid(Block block)
    {
        foreach (Transform item in block.transform)
        {
            Vector2 pos = Rounding.Round(item.position);

            grid[(int)pos.x, (int)pos.y] = item;
        }
    }

    public void clearAllRows()
    {
        for (int y = 0; y < height; y++)
        {
            if (isComplete(y))
            {
                ClearRow(y);

                ShiftDownBlock(y + 1);

                y--;
            }
        }
    }

    bool isComplete(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if(grid[x,y] == null)
            {
                return false;
            }
        }
        return true;
    }
    void ClearRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)
            {
                Destroy(grid[x,y].gameObject);
            }
                grid[x, y] = null;
        }
    }

    void ShiftDownBlock(int startY)
    {
        for (int y = startY; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x,y] != null)
                {
                grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].position += new Vector3(0, -1, 0);

                }
            }
        }
    }

    public bool OverLimit(Block block)
    {
        foreach (Transform item in block.transform)
        {
            if (item.transform.position.y >= height - header)
            {
                return true;
            }
        }
        return false;
    }
}
