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

    private LevelFactory _levelFactory;

    private Vector3Int _selectedBlock;

    private void Start()
    {
        
        Debug.Log("Generate a new level");
        //_levelManager.SetLevel(_objectGridFactory.GenerateLevelFromData(_floorGenData, _levelSpacing));
        //_levelManager.SetMesgManager(_meshManager);
        ObjectGrid objectGrid;
        MeshManager meshManager;

        _levelFactory.GenerateLevelFromData(_floorGenData, _levelSpacing, out objectGrid, out meshManager);

        _levelManager.SetLevel(objectGrid);
        _levelManager.SetMeshManager(meshManager);


        Debug.Log("Spawn a block");
        _selectedBlock = _levelManager.SpawnBlock(new Vector3Int(1, 1, 0));
        _levelManager.SpawnBlock(new Vector3Int(3, 1, 0));

        Debug.Log("Old " + _levelManager.GetBlock(new Vector3Int(1, 1, 0)).Occupied + "New " + _levelManager.GetBlock(new Vector3Int(2, 1, 0)).Occupied);

        _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.right);
        _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.up);
        _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.right);
        _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.right);
        _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.right);
        _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.right);




        Debug.Log("Old " + _levelManager.GetBlock(new Vector3Int(1, 1, 0)).Occupied + "New " + _levelManager.GetBlock(new Vector3Int(2, 1, 0)).Occupied);

        //_levelManager.SpawnBlock(new Vector3Int(1, 1, 0));
        //_levelManager.MoveBlock(new Vector3Int(1, 1, 0), Vector3Int.up);

        //Debug.Log("Old " + _levelManager.GetBlock(new Vector3Int(1, 1, 0)) + "New " + _levelManager.GetBlock(new Vector3Int(1, 2, 0)));

        if (_debug) _levelManager.Debug();

    }
    [Inject]
    public void Construct(LevelFactory gridF)
    {
        _levelFactory = gridF;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.down);
        if (Input.GetKeyDown(KeyCode.UpArrow)) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.up);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, Vector3Int.right);

        if (Input.GetKeyDown(KeyCode.W)) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, new Vector3Int(0,0,1));
        if (Input.GetKeyDown(KeyCode.S)) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, new Vector3Int(0, 0, -1));
    }

}
