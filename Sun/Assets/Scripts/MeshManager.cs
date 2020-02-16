using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshManager
{
    private Transform[,,] _transforms;

    public MeshManager(int maxX, int maxY, int numFloors)
    {
        _transforms = new Transform[maxX,maxY,numFloors];
    }
    public void SpawnBlock(Vector3Int spawnPoint)
    {
        GameObject t = GameObject.CreatePrimitive(PrimitiveType.Cube);
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
}
