using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.InputSystem;
using UnityEditor;

public class GridTest : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private List<FloorGenerationData> _floorGenData;
    [SerializeField] private int _levelSpacing = 1;


    [SerializeField] private bool _debug;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject _prefab;


    private LevelFactory _levelFactory;

    private ObjectGrid _objectGrid;
    private MeshManager _meshManager;

    private Keyboard _kb;
    private Mouse _m;

    private Vector3Int _selectedBlock;
    private Vector3Int _input;

    private bool[,,] _nodes;



    private void OnEnable()
    {
        Generatelevel();
    }

    private void Start()
    {
        _objectGrid = null;
        _meshManager = null;

        _kb = InputSystem.GetDevice<Keyboard>();
        _m = InputSystem.GetDevice<Mouse>();
    }

    [Inject]
    public void Construct(LevelFactory gridF)
    {
        _levelFactory = gridF;
    }

    public void SpawnBlock(Vector3Int spawnPoint)
    {
        _selectedBlock = _levelManager.SpawnBlock(spawnPoint, _prefab);
    }
    private void ShootLaser()
    {
        _levelManager.FireLasersFromBlock(_selectedBlock);
    }

    public void Generatelevel()
    {
        Debug.Log("Generate a new level");

        if (_levelFactory == null) _levelFactory = new LevelFactory();

        _levelFactory.GenerateLevelFromData(_floorGenData, out _objectGrid, out _meshManager);

        _levelManager.SetLevel(_objectGrid);
        _levelManager.SetMeshManager(_meshManager);

        if (_debug) DrawDebug();
    }
    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (_m.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(_m.position.ReadValue());

            if (Physics.Raycast(ray, out hit))
            {
                _selectedBlock = Vector3Int.CeilToInt(hit.transform.position);
                Debug.Log(_selectedBlock);
            }
        }

        if (_selectedBlock == null) return;

        _input = Vector3Int.zero;

        if (_kb.sKey.wasPressedThisFrame) _input.y--;
        if (_kb.wKey.wasPressedThisFrame) _input.y++;
        if (_kb.aKey.wasPressedThisFrame) _input.x--;
        if (_kb.dKey.wasPressedThisFrame) _input.x++;

        if (_kb.qKey.wasPressedThisFrame) SpawnBlock(Vector3Int.zero);

        if (_kb.rKey.wasPressedThisFrame) ShootLaser();

        if (_input != Vector3Int.zero) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, _input);

    }

    public void ClearAllBlocks()
    {
        _levelManager.ClearAllBlocks();
    }
    private void OnApplicationQuit()
    {
        DrawDebug();
    }

    public void DrawDebug()
    {
       _nodes =  _levelManager.DebugDrawNodes();
       SceneView.RepaintAll();
    }

    #region GizmoStuff

    private void OnDrawGizmos()
    {
        if (_nodes != null)
        {
            for (int f = 0; f < _nodes.GetLength(2); f++)
            {
                for (int x = 0; x < _nodes.GetLength(0); x++)
                {
                    for (int y = 0; y < _nodes.GetLength(1); y++)
                    {
                        if (_nodes[x, y, f]) Gizmos.DrawCube(new Vector3(x, y, f), Vector3.one * 0.2f);
                        else Gizmos.DrawSphere(new Vector3(x, y, f), 0.05f);
                    }
                }
            }
        }
    }
    #endregion

}
