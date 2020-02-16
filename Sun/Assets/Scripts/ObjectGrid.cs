using System.Collections.Generic;
using UnityEngine;

public class ObjectGrid
{
    private bool[,,] _useableCells;
    private Node[,,] _nodes;
    //private int maxX, maxY;

    public ObjectGrid(List<FloorGenerationData> data, int maxX, int maxY, int spacing)
    
    {
        //find the required array size
        _nodes = new Node[maxX, maxY, data.Count];
        _useableCells = new bool[maxY, maxY, data.Count];

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
                        _nodes[x,y,f] = new Node(x, y, f);
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
    public bool AddBlock(Vector3Int spawnPoint)
    {
        //If the node is empty add a block
        Debug.Log("valid = " + IsValidPoint(spawnPoint));
        if (IsValidPoint(spawnPoint))
        {
            if (!_nodes[spawnPoint.x, spawnPoint.y, spawnPoint.z].Occupied)
            {
                _nodes[spawnPoint.x, spawnPoint.y, spawnPoint.z].Occupied = true;
                return true;
            }
            else
            {
                Debug.LogWarning("Can't spawn block, node already occupied.");
            }
        }
        return false;
    }

    public Node GetNodeAtPosition(Vector3Int pos)
    {
        return _useableCells[pos.x, pos.y, pos.z] ? _nodes[pos.x, pos.y, pos.z] : null;
    }

    //move a block from one space to another if one exists

    public bool MoveBlock(Vector3Int blockToMove, Vector3Int direction)
    {
        if (IsValidPoint(blockToMove) && IsValidPoint(blockToMove + direction)) 
        { 
            direction.Clamp(Vector3Int.zero, Vector3Int.one);

            _nodes[blockToMove.x, blockToMove.y, blockToMove.z].Occupied = false;
            blockToMove += direction;
            _nodes[blockToMove.x, blockToMove.y, blockToMove.z].Occupied = true;

            //Vector3Int newPosition = blockToMove += direction;
            //_nodes[newPosition.x, newPosition.y, newPosition.z].Occupied = true;
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

public class Node
{
    private Vector3Int _position;
    private bool _occupied;
    public Node(Vector3Int pos)
    {
        _position = pos;
        _occupied = false;
    }

    public Node(int x, int y, int z)
    {
        _position = new Vector3Int(x, y, z);
        _occupied = false;
    }

    public Vector3Int Position
    {
        get => _position;
    }

    public bool Occupied
    {
        get => _occupied;
        set => _occupied = value;
    }


}

