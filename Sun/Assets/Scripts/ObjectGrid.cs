using System.Collections.Generic;
using UnityEngine;

public class ObjectGrid<TBlockData>
{
    private bool[,,] _useableCells;
    private TBlockData[,,] _blocksInLevel;

    public ObjectGrid(List<FloorGenerationData> data, int maxX, int maxY, int spacing)
    
    {
        //find the required array size
        _useableCells = new bool[maxX, maxY, data.Count];
        _blocksInLevel = new TBlockData[maxX, maxY, data.Count];

        for(int f = 0; f < data.Count;f++)
        {
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if(x >= data[f].startX && x < data[f].startX + data[f].width && y >= data[f].startY && y < data[f].startY + data[f].height)
                    {
                        _useableCells[x, y, f] = true;
                    }
                    else
                    {
                        _useableCells[x, y, f] = false;
                    }
                }
            }
        }

    }

    //Add an interactable block to the grid.
    public bool AddBlock(Vector3Int spawnPoint, TBlockData block)
    {
        //If the node is empty add a block
        if (IsValidPoint(spawnPoint))
        {
            if(_blocksInLevel[spawnPoint.x, spawnPoint.y, spawnPoint.z] == null)
            {
                _blocksInLevel[spawnPoint.x, spawnPoint.y, spawnPoint.z] = block;
                return true;
            }
            else
            {
                Debug.LogWarning("Can't spawn block, node already occupied.");
            }
        }
        return false;
    }

    public TBlockData GetNodeAtPosition(Vector3Int pos)
    {
        TBlockData block = _blocksInLevel[pos.x, pos.y, pos.z];
        if(block != null)
        {
            return block;
        }
        else
        {
            return default;
        }
    }

    //move a block from one space to another if one exists

    public bool MoveBlock(Vector3Int pos, Vector3Int dir)
    {
        if (IsValidPoint(pos) && IsValidPoint(pos + dir)) 
        {
            if (_blocksInLevel[pos.x + dir.x, pos.y + dir.y, pos.z + dir.z] != null) return false;

            dir.Clamp(Vector3Int.one * -1, Vector3Int.one);

            TBlockData block = _blocksInLevel[pos.x, pos.y, pos.z];
            _blocksInLevel[pos.x, pos.y, pos.z] = default;
            pos += dir;
            _blocksInLevel[pos.x, pos.y, pos.z] = block;
            return true;
        }

        return false;

    }
    
    //Function to check a direction from a block

    public TBlockData CheckDirection(Vector3Int pos, Vector3Int dir)
    {
        if (dir == Vector3Int.zero) return default;

        dir.Clamp(Vector3Int.one * -1, Vector3Int.one);

        TBlockData hit = default;
        while(IsValidPoint(pos + dir))
        {
            Debug.DrawLine(pos, pos + dir, Color.red, 2f);
            hit = GetNodeAtPosition(pos += dir);
            if (hit != default) break;
        }
        if(hit != default)
        {
            Debug.Log("hit");
        }
        else
        {
            Debug.Log("no hit");
        }
        return hit;
    }


    public bool IsValidPoint(Vector3Int point)
    {
        if (point.x < 0 || point.x >= _useableCells.GetLength(0) || point.y < 0 || point.y >= _useableCells.GetLength(1) || point.z < 0 || point.z >= _useableCells.GetLength(2))  return false;

        return _useableCells[point.x, point.y, point.z];
    }

    public bool[,,] GetFloors()
    {
        return _useableCells;
    }
}


