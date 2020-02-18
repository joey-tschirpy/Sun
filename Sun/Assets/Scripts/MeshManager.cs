using System.Collections.Generic;
using UnityEngine;

public class MeshManager
{
    private Transform[,,] _transforms;
    private List<GameObject> _objects;

    public MeshManager(int maxX, int maxY, int numFloors)
    {
        _transforms = new Transform[maxX,maxY,numFloors];
        _objects = new List<GameObject>();
    }
    public void SpawnBlock(Vector3Int spawnPoint)
    {
        GameObject t = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _objects.Add(t);
        _transforms[spawnPoint.x, spawnPoint.y, spawnPoint.z] = t.transform;
        _transforms[spawnPoint.x, spawnPoint.y, spawnPoint.z].position = spawnPoint;
    }
    public void MoveBlock(Vector3Int blockToMove, Vector3Int direction)
    {
        Vector3Int newPos = blockToMove + direction;
        _transforms[newPos.x, newPos.y, newPos.z] = _transforms[blockToMove.x, blockToMove.y, blockToMove.z];
        _transforms[newPos.x, newPos.y, newPos.z].position = newPos;
        _transforms[blockToMove.x, blockToMove.y, blockToMove.z] = null;
    }

    public void Destroy()
    {
        foreach(GameObject o in _objects)
        {
            GameObject.Destroy(o);
        }
        _objects.Clear();
    }
}
