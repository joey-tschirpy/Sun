using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FloorGenerationData
{
    public int width, height;
    public int startX, startY;
}
public class LevelManager : MonoBehaviour
{

    private ObjectGrid _levelGrid;
    private MeshManager _meshManager;
    private int _activeFloor = 0;

    private bool[,,] _floors;
    
    public void SetLevel(ObjectGrid levelGrid)
    {
       _levelGrid = levelGrid;
    }

    public void SetMeshManager(MeshManager meshManager)
    {
        _meshManager = meshManager;
    }

    //function to spawn a new block at a position

    public Vector3Int SpawnBlock(Vector3Int spawnPoint)
    {
        if(_levelGrid.AddBlock(spawnPoint))
            if (_meshManager != null) _meshManager.SpawnBlock(spawnPoint);

        return spawnPoint;
        
    }

    public Node GetBlock(Vector3Int pos)
    {
        return _levelGrid.GetNodeAtPosition(pos);
    }

    //function to move a block
    public Vector3Int MoveBlock(Vector3Int blockToMove, Vector3Int dir)
    {
        if(_levelGrid.MoveBlock(blockToMove, dir))
        {
            if (_meshManager != null) _meshManager.MoveBlock(blockToMove, dir);
            return blockToMove + dir;
        }
        return blockToMove;

    }

    public void Debug()
    {
       _floors = _levelGrid.GetFloors();
    }

    private void OnDrawGizmos()
    {
        if(_floors != null)
        {
            for(int f = 0; f < _floors.GetLength(2);f++)
            {
                for(int x = 0; x < _floors.GetLength(0); x++)
                {
                    for(int y = 0; y < _floors.GetLength(1); y++)
                    {
                        if (_floors[x,y,f]) Gizmos.DrawCube(new Vector3(x, y, f), Vector3.one * 0.2f);
                        else Gizmos.DrawSphere(new Vector3(x, y, f), 0.05f);
                    }
                }
            }
        }
    }
}
