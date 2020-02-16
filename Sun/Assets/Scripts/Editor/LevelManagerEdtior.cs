using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelManager myTarget = (LevelManager)target;

        if(GUILayout.Button("Spawn Block"))
        {
            //myTarget.SpawnBlock();
        }
    }
}
