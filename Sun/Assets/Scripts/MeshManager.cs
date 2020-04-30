using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeshManager
{
    [SerializeField]
    private Transform[,,] _transforms;
    [SerializeField]
    private GameObject[,,] _objects;

    public MeshManager(int maxX, int maxY, int numFloors)
    {
        _transforms = new Transform[maxX, maxY, numFloors];
        _objects = new GameObject[maxX, maxY, numFloors];

        LevelManager events = Object.FindObjectOfType<LevelManager>();

        events.OnBlockSpawn += SpawnBlock;
        events.OnBlockMove += MoveBlock;
        events.OnClearAllBlocks += ClearAllBlocks;

    }

    public void SpawnBlock(Vector3Int spawnPoint, GameObject block)
    {
        _objects[spawnPoint.x, spawnPoint.y, spawnPoint.z] = block;
        _transforms[spawnPoint.x, spawnPoint.y, spawnPoint.z] = block.transform;
        _transforms[spawnPoint.x, spawnPoint.y, spawnPoint.z].position = spawnPoint;
    }
    public void MoveBlock(Vector3Int pos, Vector3Int dir)
    {
        Vector3Int newPos = pos + dir;
        _transforms[newPos.x, newPos.y, newPos.z] = _transforms[pos.x, pos.y, pos.z];
        _transforms[newPos.x, newPos.y, newPos.z].position = newPos;
        _transforms[pos.x, pos.y, pos.z] = null;

        _objects[newPos.x, newPos.y, newPos.z] = _objects[pos.x, pos.y, pos.z];
        _objects[pos.x, pos.y, pos.z] = null;
    }

    public void ClearAllBlocks()
    {
        for (int x = 0; x < _transforms.GetLength(0); x++)
        {
            for (int y = 0; y < _transforms.GetLength(1); y++)
            {
                for (int z = 0; z < _transforms.GetLength(2); z++)
                {
                    if (_transforms[x, y, z] != null)
                    {
                        GameObject.DestroyImmediate(_transforms[x, y, z].gameObject);
                        _transforms[x, y, z] = null;
                    }
                }
            }
        }
    }

    public void Destroy()
    {
        LevelManager events = Object.FindObjectOfType<LevelManager>();

        events.OnBlockSpawn -= SpawnBlock;
        events.OnBlockMove -= MoveBlock;
        events.OnClearAllBlocks -= ClearAllBlocks;

        ClearAllBlocks();
    }

}
