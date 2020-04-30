using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public delegate void SpawnBlockHandler(Vector3Int pos, GameObject newBlock);
    public SpawnBlockHandler OnBlockSpawn;

    public delegate void MoveBlockHandler(Vector3Int pos, Vector3Int dir);
    public MoveBlockHandler OnBlockMove;

    public delegate void ClearBlocksHandler();
    public ClearBlocksHandler OnClearAllBlocks;

    [SerializeField] LevelDataScriptableObject _levelData;

    private void Start()
    {
        GenerateArraysOnStart();
    }
    public void SetLevel(ObjectGrid levelGrid)
    {
        _levelData.LevelObjectGrid = levelGrid;
    }
    
    public void SetMeshManager(MeshManager meshManager)
    {
        _levelData.LevelMeshManager = meshManager;
    }

    //function to spawn a new block at a position

    public Vector3Int SpawnBlock(Vector3Int spawnPoint, GameObject prefab)
    {
        if(OnBlockSpawn != null
            && _levelData.LevelObjectGrid.IsValidPoint(spawnPoint)
            && !_levelData.LevelObjectGrid.IsOccupied(spawnPoint))
        {
            GameObject newBlock = Instantiate(prefab);

            _levelData.BlocksInLevel.Add(newBlock);
            OnBlockSpawn(spawnPoint, newBlock);
        }
        return spawnPoint;
    }

    public GameObject GetBlock(Vector3Int pos)
    {
        return _levelData.LevelObjectGrid.GetNodeAtPosition(pos);
    }

    //function to move a block
    public Vector3Int MoveBlock(Vector3Int pos, Vector3Int dir)
    {
        dir.Clamp(Vector3Int.one * -1, Vector3Int.one);

        if(_levelData.LevelObjectGrid.IsValidPoint(pos + dir) && !_levelData.LevelObjectGrid.IsOccupied(pos + dir))
        {
            OnBlockMove?.Invoke(pos, dir);
            return pos += dir;
        }
        return pos;
    }

    public FaceUtils.HitData FireLasersFromBlock(Vector3Int pos)
    {
        ILaserInteractable block = _levelData.LevelObjectGrid.GetNodeAtPosition(pos).GetComponent<ILaserInteractable>();

        if (block != null)
        {
            FaceUtils.Direction[] dirs = block.HandleLaserInput();
            Vector3Int dir = FaceUtils.GetDirectionFromFace(block.GetOutputFace(), dirs[0]);
            return _levelData.LevelObjectGrid.CheckDirection(pos, dir, block.GetOutputFace());
        }
        return default;
    }

    public void GenerateArraysOnStart()
    {
        if(_levelData.BlocksInLevel != null)
        {
            foreach (GameObject o in _levelData.BlocksInLevel)
            {
                OnBlockSpawn?.Invoke(Vector3Int.RoundToInt(o.transform.position), o);
            }
        }
    }

    public void ClearAllBlocks()
    {
        _levelData.BlocksInLevel.Clear();
        OnClearAllBlocks?.Invoke();
    }

    public bool[,,] DebugDrawNodes()
    {
        return _levelData.LevelObjectGrid.GetFloors();
    }
}
