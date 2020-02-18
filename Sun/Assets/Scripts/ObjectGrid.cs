using System.Collections.Generic;
using UnityEngine;

public class ObjectGrid<T>
{
    private bool[,,] _useableCells;
    private Dictionary<Vector3Int, T> _blocksInLevel;

    public ObjectGrid(List<FloorGenerationData> data, int maxX, int maxY, int spacing)
    
    {
        //find the required array size
        _useableCells = new bool[maxY, maxY, data.Count];
        _blocksInLevel = new Dictionary<Vector3Int, T>();

        //foreach (var d in data)
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
    public bool AddBlock(Vector3Int spawnPoint, T prefab)
    {
        //If the node is empty add a block
        Debug.Log("valid = " + IsValidPoint(spawnPoint));
        if (IsValidPoint(spawnPoint))
        {
            if(!_blocksInLevel.ContainsKey(spawnPoint))
            {
                _blocksInLevel.Add(spawnPoint, prefab);
                return true;
            }
            else
            {
                Debug.LogWarning("Can't spawn block, node already occupied.");
            }
        }
        return false;
    }

    public T GetNodeAtPosition(Vector3Int pos)
    {
        T block;
        if(_blocksInLevel.TryGetValue(pos, out block))
        {
            return block;
        }
        else
        {
            return default(T);
        }
    }

    //move a block from one space to another if one exists

    public bool MoveBlock(Vector3Int position, Vector3Int direction)
    {
        if (IsValidPoint(position) && IsValidPoint(position + direction)) 
        {
            if (_blocksInLevel.ContainsKey(position + direction)) return false;

            direction.Clamp(Vector3Int.one * -1, Vector3Int.one);
            T block = _blocksInLevel[position];
            _blocksInLevel.Remove(position);
            position += direction;
            _blocksInLevel.Add(position, block);

            return true;
        }

        return false;

    }
    
    //Function to check a direction from a block


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


