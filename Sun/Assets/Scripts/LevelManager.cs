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

    [SerializeField]
    private List<GameObject> _blocksInLevel;

    private ObjectGrid _objectGrid;
    private MeshManager _meshManager;

    private void Start()
    {
        GenerateArraysOnStart();
    }
    public void SetLevel(ObjectGrid levelGrid)
    {
        _objectGrid = levelGrid;
    }
    
    public void SetMeshManager(MeshManager meshManager)
    {
        _meshManager = meshManager;
    }

    //function to spawn a new block at a position

    public Vector3Int SpawnBlock(Vector3Int spawnPoint, GameObject prefab)
    {
        if (_blocksInLevel == null) _blocksInLevel = new List<GameObject>();

        if(OnBlockSpawn != null && _objectGrid.IsValidPoint(spawnPoint) && !_objectGrid.IsOccupied(spawnPoint))
        {
            GameObject newBlock = Instantiate(prefab);
            _blocksInLevel.Add(newBlock);

            OnBlockSpawn(spawnPoint, newBlock);
        }
        return spawnPoint;
    }

    public GameObject GetBlock(Vector3Int pos)
    {
        return _objectGrid.GetNodeAtPosition(pos);
    }

    //function to move a block
    public Vector3Int MoveBlock(Vector3Int pos, Vector3Int dir)
    {
        dir.Clamp(Vector3Int.one * -1, Vector3Int.one);
        if (_objectGrid.IsValidPoint(pos + dir) && !_objectGrid.IsOccupied(pos + dir))
        {
            OnBlockMove?.Invoke(pos, dir);
            return pos += dir;
        }
        return pos;
    }

    public void FireLasersFromBlock(Vector3Int pos)
    {
        ILaserInteractable block = _objectGrid.GetNodeAtPosition(pos).GetComponent<ILaserInteractable>();

        if(block != null)
        {
            FaceUtils.Direction[] dirs = block.HandleLaserInput();
            Vector3Int dir = FaceUtils.GetDirectionFromFace(block.GetOutputFace(), dirs[0]);
            FaceUtils.HitData result = _objectGrid.CheckDirection(pos, dir, block.GetOutputFace());
        }

    }

    public void GenerateArraysOnStart()
    {
        if(_blocksInLevel != null)
        {
            foreach (GameObject o in _blocksInLevel)
            {
                OnBlockSpawn?.Invoke(Vector3Int.RoundToInt(o.transform.position), o);
            }
        }
    }

    public void ClearAllBlocks()
    {
        _blocksInLevel.Clear();
        OnClearAllBlocks?.Invoke();
    }

    public bool[,,] DebugDrawNodes()
    {
        GenerateArraysOnStart();
        return _objectGrid.GetFloors();
    }
}
