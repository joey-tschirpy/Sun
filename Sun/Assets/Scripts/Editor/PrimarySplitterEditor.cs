using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(PrimarySplitter))]
public class PrimarySplitterEditor : LaserObjectEditor
{
    public override void OnInspectorGUI()
    {
        required = new List<ModuleType>();
        required.Add(ModuleType.ManipulationInput);
        required.Add(ModuleType.Output);

        base.OnInspectorGUI();
    }
}
