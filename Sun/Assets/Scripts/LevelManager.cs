using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private ObjectGrid _objectGrid;
    private MeshManager _meshManager;

    private bool[,,] _floors;

    public void SetLevel(ObjectGrid levelGrid)
    {
        _objectGrid = levelGrid;
        //_laserManager = new LaserManager(levelGrid);
    }

    public void SetMeshManager(MeshManager meshManager)
    {
        _meshManager = meshManager;
    }

    //function to spawn a new block at a position

    public Vector3Int SpawnBlock(Vector3Int spawnPoint, GameObject prefab)
    {
        if (_objectGrid.GetNodeAtPosition(spawnPoint) == null && _objectGrid.IsValidPoint(spawnPoint))
        {
            GameObject newBlock = Instantiate(prefab);

            if (_objectGrid.AddBlock(spawnPoint, newBlock))
            {
                if (_meshManager != null) _meshManager.AddBlock(spawnPoint, newBlock);
                //Debug.Log("Invalid spawn");
            }

        }
        return spawnPoint;
    }

    public bool GetBlock(Vector3Int pos)
    {
        return _objectGrid.GetNodeAtPosition(pos);
    }

    //function to move a block
    public Vector3Int MoveBlock(Vector3Int blockToMove, Vector3Int dir)
    {
        if (_objectGrid.MoveBlock(blockToMove, dir))
        {
            //_laserManager.CheckActivePath(blockToMove, dir);
            if (_meshManager != null)
            {
                _meshManager.MoveBlock(blockToMove, dir);
                return blockToMove + dir;
            }
        }
        return blockToMove;
    }

    public void SetOutputFace(Vector3Int pos, FaceUtils.Face f)
    {
        ILaserInteractable block = _objectGrid.GetNodeAtPosition(pos).GetComponent<ILaserInteractable>();
        if (block != null)
        {
            block.SetOutputFace(f);
            //_laserManager.CheckActivePath(pos);
        }
    }

    public void SetInputFace(Vector3Int pos, FaceUtils.Face f)
    {
        ILaserInteractable block = _objectGrid.GetNodeAtPosition(pos).GetComponent<ILaserInteractable>();
        if (block != null)
        {
            block.SetInputFace(f);
            //_laserManager.CheckActivePath(pos);
        }
    }

    public void FireLasersFromBlock(Vector3Int pos)
    {
        //_laserManager.FireLasers(pos);
        ILaserInteractable block = _objectGrid.GetNodeAtPosition(pos).GetComponent<ILaserInteractable>();

        if(block != null)
        {
            FaceUtils.Direction[] dirs = block.HandleLaserInput();
            Vector3Int dir = FaceUtils.GetDirectionFromFace(block.GetOutputFace(), dirs[0]);
            FaceUtils.HitData result = _objectGrid.CheckDirection(pos, dir, block.GetOutputFace());

            Debug.Log(result.FaceHit);
        }

    }


    #region GizmoStuff
    public void Test()
    {
        _floors = _objectGrid.GetFloors();
    }

    private void OnDrawGizmos()
    {
        if (_floors != null)
        {
            for (int f = 0; f < _floors.GetLength(2); f++)
            {
                for (int x = 0; x < _floors.GetLength(0); x++)
                {
                    for (int y = 0; y < _floors.GetLength(1); y++)
                    {
                        if (_floors[x, y, f]) Gizmos.DrawCube(new Vector3(x, y, f), Vector3.one * 0.2f);
                        else Gizmos.DrawSphere(new Vector3(x, y, f), 0.05f);
                    }
                }
            }
        }
    }
    #endregion
}
