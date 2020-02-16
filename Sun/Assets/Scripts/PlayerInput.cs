using UnityEngine;
using Zenject;

[RequireComponent(typeof(LevelManager))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    private Vector2Int _input;

    private void Awake()
    {
        _input = new Vector2Int(0, 0);
    }

    private void Update()
    {
    ;
        if (Input.GetKeyDown(KeyCode.RightArrow)) _input.x++;
        if (Input.GetKeyDown((KeyCode.LeftArrow))) _input.x--;
        if (Input.GetKeyDown(KeyCode.UpArrow)) _input.y++;
        if (Input.GetKeyDown(KeyCode.DownArrow)) _input.y--;

        //if (_input.x != 0 || _input.y != 0) _levelManager.MoveBlock();

        _input = Vector2Int.zero;

    }
}
