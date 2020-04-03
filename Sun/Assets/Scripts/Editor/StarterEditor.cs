using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Starter))]
public class StarterEditor : LaserObjectEditor
{
    public override void OnInspectorGUI()
    {
        required = new List<ModuleType>();
        required.Add(ModuleType.Output);

        restricted = new List<ModuleType>();
        restricted.Add(ModuleType.PowerInput);
        restricted.Add(ModuleType.ManipulationInput);

        var labelStyle = new GUIStyle(GUI.skin.GetStyle("BoldLabel"));
        labelStyle.alignment = TextAnchor.MiddleLeft;

        base.OnInspectorGUI();

        // Setting output lasers
        var outputLasersSP = serializedObject.FindProperty("outputLasers");
        var directions = (Direction[])System.Enum.GetValues(typeof(Direction));

        outputLasersSP.arraySize = directions.Length;

        GUILayout.Label("Output Lasers", labelStyle, GUILayout.Height(RowHeight));
        foreach (var direction in directions)
        {
            EditorGUILayout.PropertyField(outputLasersSP.GetArrayElementAtIndex((int)direction), new GUIContent(direction.ToString()));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
