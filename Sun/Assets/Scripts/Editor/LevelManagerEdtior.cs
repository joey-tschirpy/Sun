using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridTest))]
public class GridTestEditor : Editor
{

    [SerializeField] private Vector3Int _spawnPoint;
    private Vector3Int _direction;
    private GridTest _myTarget;

    private void Awake()
    {
        _myTarget = (GridTest)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate Level"))
        {
            _myTarget.Generatelevel();
        }


        _spawnPoint =  EditorGUILayout.Vector3IntField("Spawn Point", _spawnPoint);


        if (GUILayout.Button("Spawn Block"))
        {
            _myTarget.SpawnBlock(_spawnPoint);
        }


        _direction = EditorGUILayout.Vector3IntField("Check Direction", _direction);
        if (GUILayout.Button("Check"))
        {
            _myTarget.CheckDirection(_direction);
        }
    }
}
