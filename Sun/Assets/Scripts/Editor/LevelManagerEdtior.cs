using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridTest))]
public class GridTestEditor : Editor
{
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
    }
}
