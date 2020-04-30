using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridTest)), InitializeOnLoad]
public class GridTestEditor : Editor
{
    private GridTest _myTarget;

    private Vector3Int _spawnPoint;

    private void Awake()
    {
        _myTarget = (GridTest)target;
        EditorApplication.playModeStateChanged += LogPlayModeState;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate Level"))
        {
            _myTarget.Generatelevel();
        }

        _spawnPoint = EditorGUILayout.Vector3IntField("spawn point", _spawnPoint);
        if(GUILayout.Button("Spawn Block"))
        {
            _myTarget.SpawnBlock(_spawnPoint);
        }


        if(GUILayout.Button("Clear All Blocks"))
        {
            _myTarget.ClearAllBlocks();
        }
    }

    private void LogPlayModeState(PlayModeStateChange state)
    {
        if(state == PlayModeStateChange.EnteredEditMode)
        {
            _myTarget.DrawDebug();
        }
    }
}
