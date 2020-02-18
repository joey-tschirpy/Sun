using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridTest))]
public class GridTestEditor : Editor
{

    [SerializeField] private Vector3Int _spawnPoint;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridTest myTarget = (GridTest)target;

        _spawnPoint =  EditorGUILayout.Vector3IntField("Spawn Point", _spawnPoint);

        if(GUILayout.Button("Spawn Block"))
        {
            myTarget.SpawnBlock(_spawnPoint);
        }
    }
}
