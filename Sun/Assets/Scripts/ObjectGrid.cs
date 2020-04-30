using System.Collections.Generic;
using UnityEngine;

// --------------TO-DO-----------------------------
//remove laser manager stuff
//keep mesh manager for now
//remove all test code
//make everything run in the editor instead of play
//add dropdown menu of prefabs to spawn blocks
[System.Serializable]
public class ObjectGrid
{
    [SerializeField]
    private bool[,,] _useableCells;
    private GameObject[,,] _blocksInLevel;

    public ObjectGrid(List<FloorGenerationData> data, int maxX, int maxY, int spacing)
    {
        _useableCells = new bool[maxX, maxY, data.Count];
        _blocksInLevel = new GameObject[maxX, maxY, data.Count];

        for (int f = 0; f < data.Count; f++)
        {
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if (x >= data[f].startX && x < data[f].startX + data[f].width && y >= data[f].startY && y < data[f].startY + data[f].height)
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
        LevelManager events = Object.FindObjectOfType<LevelManager>();

        events.OnBlockSpawn += SpawnBlock;
        events.OnBlockMove += MoveBlock;
        events.OnClearAllBlocks += ClearBlocks;
    }

    //Add an interactable block to the grid.
    public void SpawnBlock(Vector3Int spawnPoint, GameObject block)
    {
        //If the node is empty add a block
        if (IsValidPoint(spawnPoint))
        {
            _blocksInLevel[spawnPoint.x, spawnPoint.y, spawnPoint.z] = block;
        }
    }

    public GameObject GetNodeAtPosition(Vector3Int pos)
    {
        if (IsValidPoint(pos)) return _blocksInLevel[pos.x, pos.y, pos.z];

        return null;
    }

    public GameObject GetNodeAtPosition(int x, int y, int z)
    {
        if (IsValidPoint(x, y, z)) return _blocksInLevel[x, y, z];

        return null;
    }

    //move a block from one space to another if one exists

    public void MoveBlock(Vector3Int pos, Vector3Int dir)
    {
            GameObject block = _blocksInLevel[pos.x, pos.y, pos.z];
            _blocksInLevel[pos.x, pos.y, pos.z] = null;
            pos += dir;
            _blocksInLevel[pos.x, pos.y, pos.z] = block;
    }

    //Function to check a direction from a block
    public FaceUtils.HitData CheckDirection(Vector3Int pos, Vector3Int dir, FaceUtils.Face face) // position, face, direction
    {
        bool horizontalStep = face == FaceUtils.Face.left || face == FaceUtils.Face.right;

        if (dir == Vector3Int.zero)
        {
            return default;
        }

        dir.Clamp(Vector3Int.one * -1, Vector3Int.one);
     
        if (dir.sqrMagnitude == 1) // Direction is only along one axis
        {
            return CheckAlongAxis(pos, dir);
        }
        else //Direction is diagonal
        {
            return CheckAlongDiagonal(pos, dir, horizontalStep);
        }
    }

    private FaceUtils.HitData CheckAlongAxis(Vector3Int pos, Vector3Int dir)
    {
        GameObject currentNode;
        while (IsValidPoint(pos + dir))
        {
            currentNode = GetNodeAtPosition(pos += dir);
            DrawDebugCells(pos.x, pos.y);
            if (currentNode != default) //If current node is not empty.
            {
                return new FaceUtils.HitData(currentNode,FaceUtils.ConvertDirectionToFace(dir * -1));
            }
        }
        return default;
    }

    private FaceUtils.HitData CheckAlongDiagonal(Vector3Int pos, Vector3Int dir, bool horizontalStep)
    {
        int x = pos.x;
        int y = pos.y;

        GameObject currentNode;

        if (horizontalStep) x += dir.x;
        else y += dir.y;

        while (IsValidPoint(x, y, pos.z))
        {
            DrawDebugCells(x, y);
            currentNode = GetNodeAtPosition(x, y, pos.z);
            if (currentNode != default)
            {
                if (horizontalStep)
                {
                    return new FaceUtils.HitData(currentNode, FaceUtils.ConvertDirectionToFace(new Vector3Int(-dir.x,0,0)));
                }
                else
                {
                    return new FaceUtils.HitData(currentNode, FaceUtils.ConvertDirectionToFace(new Vector3Int(0, -dir.y, 0)));
                }
            }

            horizontalStep = !horizontalStep;
            if (horizontalStep) x += dir.x;
            else y += dir.y;
        }
        return default;
    }


    public bool IsValidPoint(Vector3Int point)
    {

        if (point.x < 0 || point.x >= _useableCells.GetLength(0) || point.y < 0 || point.y >= _useableCells.GetLength(1) || point.z < 0 || point.z >= _useableCells.GetLength(2)) return false;
        return _useableCells[point.x, point.y, point.z];
    }

    public bool IsOccupied(Vector3Int point)
    {
        if (_blocksInLevel[point.x, point.y, point.z] != null) return true;
        return false;
    }

    public bool IsValidPoint(int x, int y, int z)
    {
        if (x < 0 || x >= _useableCells.GetLength(0) || y < 0 || y >= _useableCells.GetLength(1) || z < 0 || z >= _useableCells.GetLength(2)) return false;

        return _useableCells[x, y, z];
    }

    public void ClearBlocks()
    {
        _blocksInLevel = new GameObject[_blocksInLevel.GetLength(0), _blocksInLevel.GetLength(1), _blocksInLevel.GetLength(2)];
    }

    public bool[,,] GetFloors()
    {
        return _useableCells;
    }

    private void DrawDebugCells(float x, float y)
    {
        float duration = 1f;

        Vector3 topLeft = new Vector3(x - 0.5f, y + 0.5f);
        Vector3 topRight = new Vector3(x + 0.5f, y + 0.5f);
        Vector3 bottomLeft = new Vector3(x - 0.5f, y - 0.5f);
        Vector3 bottomRight = new Vector3(x + 0.5f, y - 0.5f);

        Debug.DrawLine(topLeft, topRight, Color.red, duration);
        Debug.DrawLine(topRight, bottomRight, Color.red, duration);
        Debug.DrawLine(bottomRight, bottomLeft, Color.red, duration);
        Debug.DrawLine(bottomLeft, topLeft, Color.red, duration);

    }

    public void Destroy()
    {
        LevelManager events = Object.FindObjectOfType<LevelManager>();

        events.OnBlockSpawn -= SpawnBlock;
        events.OnBlockMove -= MoveBlock;
        events.OnClearAllBlocks -= ClearBlocks;
    }

}


