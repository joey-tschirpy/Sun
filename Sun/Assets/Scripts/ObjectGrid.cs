using System.Collections.Generic;
using UnityEngine;

public class ObjectGrid
{
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

    }

    //Add an interactable block to the grid.
    public bool AddBlock(Vector3Int spawnPoint, GameObject block)
    {
        //If the node is empty add a block
        if (IsValidPoint(spawnPoint))
        {
            _blocksInLevel[spawnPoint.x, spawnPoint.y, spawnPoint.z] = block;
            return true;
        }

        return false;
    }

    public GameObject GetNodeAtPosition(Vector3Int pos)
    {
        if (IsValidPoint(pos)) return _blocksInLevel[pos.x, pos.y, pos.z];

        return default;
    }

    public GameObject GetNodeAtPosition(int x, int y, int z)
    {
        if (IsValidPoint(x, y, z)) return _blocksInLevel[x, y, z];

        return default;
    }

    //move a block from one space to another if one exists

    public bool MoveBlock(Vector3Int pos, Vector3Int dir)
    {
        if (IsValidPoint(pos) && IsValidPoint(pos + dir))
        {
            dir.Clamp(Vector3Int.one * -1, Vector3Int.one);

            GameObject block = _blocksInLevel[pos.x, pos.y, pos.z];
            _blocksInLevel[pos.x, pos.y, pos.z] = default;
            pos += dir;
            _blocksInLevel[pos.x, pos.y, pos.z] = block;
            return true;
        }

        return false;

    }

    //Function to check a direction from a block


    public GameObject CheckDirection(Vector3Int pos, Vector3Int dir, FaceUtils.Face face, out Vector3Int hitPos, out bool horizontalStep, ref HashSet<Vector3Int> laserPath) // position, face, direction
    {
        hitPos = new Vector3Int(-1, -1, -1);
        horizontalStep = face == FaceUtils.Face.left || face == FaceUtils.Face.right;

        if (dir == Vector3Int.zero)
        {
            return default;
        }

        dir.Clamp(Vector3Int.one * -1, Vector3Int.one);

        GameObject hit;

        if (dir.magnitude == 1) // Direction is only along one axis
        {
            CheckAlongAxis(pos, dir, out hitPos, out hit, ref laserPath);
        }
        else //Direction is diagonal
        {
            CheckAlongDiagonal(pos, dir, horizontalStep, out hitPos, out hit, ref laserPath);
        }

        return hit;
    }

    private void CheckAlongAxis(Vector3Int pos, Vector3Int dir, out Vector3Int hitPos, out GameObject hit, ref HashSet<Vector3Int> laserPath)
    {
        hit = default;
        while (IsValidPoint(pos + dir))
        {
            hit = GetNodeAtPosition(pos += dir);
            laserPath.Add(pos);
            DrawDebugCells(pos.x, pos.y);
            if (hit != default)
            {
                hitPos = pos;
                return;
            }
        }
        hitPos = pos;
    }

    private void CheckAlongDiagonal(Vector3Int pos, Vector3Int dir, bool horizontalStep, out Vector3Int hitPos, out GameObject hit, ref HashSet<Vector3Int> laserPath)
    {
        hit = default;
        int x = pos.x;
        int y = pos.y;

        if (horizontalStep) x += dir.x;
        else y += dir.y;

        while (IsValidPoint(x, y, pos.z))
        {
            DrawDebugCells(x, y);
            laserPath.Add(new Vector3Int(x, y, pos.z));
            hit = GetNodeAtPosition(x, y, pos.z);
            if (hit != default)
            {
                hitPos = new Vector3Int(x, y, pos.z);
                return;
            }

            horizontalStep = !horizontalStep;
            if (horizontalStep) x += dir.x;
            else y += dir.y;
        }
        hitPos = new Vector3Int(x, y, pos.z);
    }


    public bool IsValidPoint(Vector3Int point)
    {
        if (point.x < 0 || point.x >= _useableCells.GetLength(0) || point.y < 0 || point.y >= _useableCells.GetLength(1) || point.z < 0 || point.z >= _useableCells.GetLength(2)) return false;

        return _useableCells[point.x, point.y, point.z];
    }

    public bool IsValidPoint(int x, int y, int z)
    {
        if (x < 0 || x >= _useableCells.GetLength(0) || y < 0 || y >= _useableCells.GetLength(1) || z < 0 || z >= _useableCells.GetLength(2)) return false;

        return _useableCells[x, y, z];
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
}


