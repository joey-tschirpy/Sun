using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GridTest : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private List<FloorGenerationData> _floorGenData;
    [SerializeField] private int _levelSpacing = 1;

    [SerializeField] private bool _debug;
    [SerializeField] private Camera camera;

    private LevelFactory _levelFactory;

    private ObjectGrid<GameObject> objectGrid;
    private MeshManager meshManager;

    private Vector3Int _selectedBlock;

    private void Start()
    {
        objectGrid = null;
        meshManager = null;

        Generatelevel();
    }

    [Inject]
    public void Construct(LevelFactory gridF)
    {
        _levelFactory = gridF;
    }

    public void SpawnBlock(Vector3Int spawnPoint)
    {
        _selectedBlock = _levelManager.SpawnBlock(spawnPoint);
    }

    public void CheckDirection(Vector3Int dir)
    {
        _levelManager.CheckDirection(_selectedBlock, dir);
    }

    public void Generatelevel()
    {
        Debug.Log("Generate a new level");



        if(meshManager != null) meshManager.Destroy();

        _levelFactory.GenerateLevelFromData(_floorGenData, _levelSpacing, out objectGrid, out meshManager);

        _levelManager.SetLevel(objectGrid);
        _levelManager.SetMeshManager(meshManager);

        if (_debug) _levelManager.Test();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.down);
        if (Input.GetKeyDown(KeyCode.UpArrow)) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.up);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.right);

        if (Input.GetKeyDown(KeyCode.W)) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, new Vector3Int(0,0,1));
        if (Input.GetKeyDown(KeyCode.S)) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, new Vector3Int(0, 0, -1));

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                _selectedBlock = Vector3Int.CeilToInt(hit.transform.position);
            }
        }
    }

}
