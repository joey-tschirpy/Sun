using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Laser))]
public class LaserDrawer : PropertyDrawer
{
    private readonly int rowHeight = 20;
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return rowHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var colorSP = property.FindPropertyRelative("color");
        var powerSP = property.FindPropertyRelative("power");

        var contentStyle = new GUIStyle(GUI.skin.GetStyle("label"));
        contentStyle.fixedHeight = rowHeight;
        contentStyle.alignment = TextAnchor.MiddleLeft;

        var textFieldStyle = new GUIStyle(GUI.skin.GetStyle("textField"));
        textFieldStyle.alignment = TextAnchor.MiddleLeft;

        var colorButtonStyle = new GUIStyle(GUI.skin.GetStyle("Button"));

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label, contentStyle);

        var spacing = 5;
        var colorRect = new Rect(position.position, (Vector2.one + Vector2.right) * rowHeight);
        var powerRect = new Rect(colorRect.position.x + colorRect.size.x + spacing, colorRect.y, colorRect.width, colorRect.height);

        // For setting the laser's color
        GUI.backgroundColor = Laser.VisualColor((LaserColor)colorSP.intValue);
        if (EditorGUI.DropdownButton(colorRect, new GUIContent(""), FocusType.Keyboard, colorButtonStyle))
        {
            GenericMenu menu = new GenericMenu();

            foreach (LaserColor laserColor in System.Enum.GetValues(typeof(LaserColor)))
            {
                menu.AddItem(new GUIContent(laserColor.ToString()), false, () =>
                {
                    colorSP.intValue = (int)laserColor;
                    property.serializedObject.ApplyModifiedProperties();
                });
            }

            menu.DropDown(colorRect);
        }
        GUI.backgroundColor = Color.white;

        // For setting the laser's power
        int currentPower = powerSP.intValue;
        string newPower = EditorGUI.TextField(powerRect, currentPower.ToString(), textFieldStyle);
        int.TryParse(newPower, out currentPower);
        powerSP.intValue = Mathf.Max(0, currentPower);

        GUI.backgroundColor = Color.white;
        EditorGUI.EndProperty();
    }
}