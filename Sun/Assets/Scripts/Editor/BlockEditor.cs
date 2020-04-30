using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestPrefab))]
public class BlockEditor : Editor
{
    private TestPrefab _myTarget;

    private void Awake()
    {
        _myTarget = (TestPrefab)target;
    }

    private void OnSceneGUI()
    {
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        switch (Event.current.GetTypeForControl(controlID))
        {
            case EventType.MouseUp:
                GUIUtility.hotControl = 0;
                Event.current.Use();
                _myTarget.SnapToGrid();            
                break;
        }
    }
}
