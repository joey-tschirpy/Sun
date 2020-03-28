using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.InputSystem;

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


    private InputMaster _controls;
    private Keyboard _kb;
    private Mouse _m;

    private Vector3Int _selectedBlock;
    private Vector3Int _input;



    private void Awake()
    {
        _controls = new InputMaster();
        //controls.Editor.MoveBlock.performed
        _controls.Editor.ShootLaser.performed += ctx => ShootLaser();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Start()
    {
        _objectGrid = null;
        _meshManager = null;

        _kb = InputSystem.GetDevice<Keyboard>();
        _m = InputSystem.GetDevice<Mouse>();

        Generatelevel();
        SpawnBlock(new Vector3Int(1, 0, 0));

    }

    [Inject]
    public void Construct(LevelFactory gridF)
    {
        _levelFactory = gridF;
    }

    public void SpawnBlock(Vector3Int spawnPoint)
    {
        _selectedBlock = _levelManager.SpawnBlock(spawnPoint, _prefab);
        Debug.Log(_selectedBlock);
    }
    private void ShootLaser()
    {
        _levelManager.FireLasersFromBlock(_selectedBlock);
    }

    public void Generatelevel()
    {
        Debug.Log("Generate a new level");


        if(_meshManager != null) _meshManager.Destroy();
        _levelFactory.GenerateLevelFromData(_floorGenData, out _objectGrid, out _meshManager);

        _levelManager.SetLevel(_objectGrid);
        _levelManager.SetMeshManager(_meshManager);

        if (_debug) _levelManager.Test();
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
            }
        }

        if (_selectedBlock == null) return;

        _input = Vector3Int.zero;

        if (_kb.sKey.wasPressedThisFrame) _input.y--;
        if (_kb.wKey.wasPressedThisFrame) _input.y++;
        if (_kb.aKey.wasPressedThisFrame) _input.x--;
        if (_kb.dKey.wasPressedThisFrame) _input.x++;

        if (_kb.leftShiftKey.isPressed)
        {
            if (_kb.rightArrowKey.wasPressedThisFrame) _levelManager.SetInputFace(_selectedBlock, FaceUtils.Face.right);
            if (_kb.leftArrowKey.wasPressedThisFrame) _levelManager.SetInputFace(_selectedBlock, FaceUtils.Face.left);
            if (_kb.upArrowKey.wasPressedThisFrame) _levelManager.SetInputFace(_selectedBlock, FaceUtils.Face.front);
            if (_kb.downArrowKey.wasPressedThisFrame) _levelManager.SetInputFace(_selectedBlock, FaceUtils.Face.back);
        }
        else
        {
            if (_kb.rightArrowKey.wasPressedThisFrame) _levelManager.SetOutputFace(_selectedBlock, FaceUtils.Face.right);
            if (_kb.leftArrowKey.wasPressedThisFrame) _levelManager.SetOutputFace(_selectedBlock, FaceUtils.Face.left);
            if (_kb.upArrowKey.wasPressedThisFrame) _levelManager.SetOutputFace(_selectedBlock, FaceUtils.Face.front);
            if (_kb.downArrowKey.wasPressedThisFrame) _levelManager.SetOutputFace(_selectedBlock, FaceUtils.Face.back);
        }

        if (_kb.qKey.wasPressedThisFrame) SpawnBlock(Vector3Int.zero);

        if (_input != Vector3Int.zero) _selectedBlock = _levelManager.MoveBlock(_selectedBlock, _input);

    }

}
