using System.Collections.Generic;
using UnityEngine;

public class MeshManager
{
    private Transform[,,] _transforms;
    private GameObject[,,] _objects;

    private Material _activeMat;
    private Material _disabledMat;



    public MeshManager(int maxX, int maxY, int numFloors)
    {
        _transforms = new Transform[maxX, maxY, numFloors];
        _objects = new GameObject[maxX, maxY, numFloors];

        _activeMat = new Material(Shader.Find("Standard"));
        _activeMat.color = Color.green;

        _disabledMat = new Material(Shader.Find("Standard"));
        _disabledMat.color = Color.red;
    }

    public void AddBlock(Vector3Int spawnPoint, GameObject block)
    {
        block.GetComponent<MeshRenderer>().sharedMaterial = _disabledMat;
        _objects[spawnPoint.x, spawnPoint.y, spawnPoint.z] = block;
        _transforms[spawnPoint.x, spawnPoint.y, spawnPoint.z] = block.transform;
        _transforms[spawnPoint.x, spawnPoint.y, spawnPoint.z].position = spawnPoint;
    }
    public void MoveBlock(Vector3Int blockToMove, Vector3Int direction)
    {
        Vector3Int newPos = blockToMove + direction;
        _transforms[newPos.x, newPos.y, newPos.z] = _transforms[blockToMove.x, blockToMove.y, blockToMove.z];
        _transforms[newPos.x, newPos.y, newPos.z].position = newPos;
        _transforms[blockToMove.x, blockToMove.y, blockToMove.z] = null;

        _objects[newPos.x, newPos.y, newPos.z] = _objects[blockToMove.x, blockToMove.y, blockToMove.z];
        _objects[blockToMove.x, blockToMove.y, blockToMove.z] = null;
    }

    public void SetActive(Vector3Int pos, bool active)
    {
        if (active)
        {
            _objects[pos.x, pos.y, pos.z].gameObject.GetComponent<MeshRenderer>().material = _activeMat;
        }
    }

    public void Destroy()
    {
        foreach (GameObject o in _objects)
        {
            GameObject.Destroy(o);
        }
        //_objects.Clear();
    }

}
