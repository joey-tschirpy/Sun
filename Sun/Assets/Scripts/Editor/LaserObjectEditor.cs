using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LaserObject), true)]
public class LaserObjectEditor : Editor
{
    protected List<ModuleType> required;
    protected List<ModuleType> restricted;

    protected readonly int RowHeight = 20;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        LaserObject laserObject = (LaserObject)target;

        var modules = serializedObject.FindProperty("moduleTypes");

        var currentModules = new List<ModuleType>();
        var missingModules = new List<ModuleType>((ModuleType[])System.Enum.GetValues(typeof(ModuleType)));

        // Assign required modules to a face if none are assigned
        if (required != null)
        {
            foreach (var requiredModule in required)
            {
                var containsModule = false;
                var blankIndex = -1;

                for (int i = 0; i < modules.arraySize; i++)
                {
                    var module = (ModuleType)modules.GetArrayElementAtIndex(i).enumValueIndex;

                    if (blankIndex < 0 && module == ModuleType.Blank)
                    {
                        blankIndex = i;
                        continue;
                    }
                    else if (module == requiredModule)
                    {
                        containsModule = true;
                        break;
                    }
                }

                if (!containsModule)
                {
                    if (blankIndex < 0)
                    {
                        Debug.LogError("No faces available for required module: " + requiredModule.ToString());
                    }
                    else
                    {
                        modules.GetArrayElementAtIndex(blankIndex).enumValueIndex = (int)requiredModule;
                    }
                }
            }
        }

        // Remove ability to select any modules that is restricted
        if (restricted != null)
        {
            foreach(var module in restricted)
            {
                missingModules.Remove(module);
            }
        }

        var faces = new List<Face>((Face[])System.Enum.GetValues(typeof(Face)));

        // Setting what modules are used
        for (int i = 0; i < modules.arraySize; i++)
        {
            var module = modules.GetArrayElementAtIndex(i);
            var moduleType = (ModuleType)module.enumValueIndex;
            if (!currentModules.Contains(moduleType))
            {
                currentModules.Add(moduleType);
                missingModules.Remove(moduleType);
            }
        }
        currentModules.Sort();
        missingModules.Sort();

        var labelStyle = new GUIStyle(GUI.skin.GetStyle("BoldLabel"));
        labelStyle.alignment = TextAnchor.MiddleLeft;

        var labelStyleMid = new GUIStyle(labelStyle);
        labelStyleMid.alignment = TextAnchor.MiddleCenter;

        var contentStyle = new GUIStyle(GUI.skin.GetStyle("label"));
        contentStyle.alignment = TextAnchor.MiddleLeft;
        var requiredColor = Color.red;

        var buttonStyle = new GUIStyle(GUI.skin.GetStyle("Button"));
        buttonStyle.fontStyle = FontStyle.Bold;

        var dropDownButtonStyle = new GUIStyle(GUI.skin.GetStyle("DropDownButton"));
        dropDownButtonStyle.fontStyle = FontStyle.Bold;

        // Setting Modules/Faces grid
        GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();

                GUILayout.BeginHorizontal();

                    GUILayout.Label("Modules", labelStyle);

                    labelStyle.normal.textColor = requiredColor;
                    GUILayout.Label("(Required)", labelStyle);
                    labelStyle.normal.textColor = Color.black;

                    GUILayout.EndHorizontal();

                for (int i = 0; i < currentModules.Count; i++)
                {
                    GUILayout.BeginHorizontal();

                        if (currentModules[i] == ModuleType.Blank || (required != null && required.Contains(currentModules[i])))
                        {
                            GUILayout.Space(RowHeight + 8); // TODO: use standard horizontal spacing
                        }
                        else if (GUILayout.Button("-", buttonStyle, GUILayout.Width(RowHeight), GUILayout.Height(RowHeight)))
                        {
                            for (int j = 0; j < modules.arraySize; j++)
                            {
                                var module = modules.GetArrayElementAtIndex(j);
                                
                                if (module.intValue == (int)currentModules[i])
                                {
                                    module.intValue = 0;
                                }
                            }
                        }

                        if (required != null)
                        {
                            contentStyle.normal.textColor = required.Contains(currentModules[i]) ? Color.red : Color.black;
                        }

                        GUILayout.Label(currentModules[i].ToString(), contentStyle, GUILayout.Height(RowHeight));

                        contentStyle.normal.textColor = Color.black;

                    GUILayout.EndHorizontal();
                }

            GUILayout.EndVertical();

            for (int i = 0; i < faces.Count; i++)
            {
                GUILayout.BeginVertical();

                    GUILayout.Label(faces[i].ToString(), labelStyleMid);

                    for (int j = 0; j < currentModules.Count; j++)
                    {
                        var module = modules.GetArrayElementAtIndex(i);
                        var onCurrentFace = currentModules[j] == (ModuleType)module.enumValueIndex;

                        GUI.color = onCurrentFace ? Color.blue : Color.white;
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("", buttonStyle, GUILayout.Width(40), GUILayout.Height(RowHeight), GUILayout.ExpandWidth(true)))
                        {
                            if (onCurrentFace)
                            {
                                var changeValue = true;

                                if (required != null && required.Contains(currentModules[j]))
                                {
                                    // Count number of module that was clicked and is required
                                    // Only change if there is more than 1 left
                                    var count = 0;
                                    for (int k = 0; k < modules.arraySize; k++)
                                    {
                                        var moduleType = (ModuleType)modules.GetArrayElementAtIndex(k).enumValueIndex;
                                        if (moduleType == currentModules[j])
                                        {
                                            count++;
                                        }
                                    }

                                    changeValue = count > 1;
                                }

                                if (changeValue)
                                {
                                    module.intValue = 0;
                                }
                            }
                            else
                            {
                                var changeValue = true;
                                
                                if (required != null && required.Contains((ModuleType)module.enumValueIndex))
                                {
                                    // Count number of module that is being removed from the face and is required
                                    // Only change if there is more than 1 left
                                    var count = 0;
                                    for (int k = 0; k < modules.arraySize; k++)
                                    {
                                        var moduleIndex = modules.GetArrayElementAtIndex(k).enumValueIndex;
                                        if (moduleIndex == module.enumValueIndex)
                                        {
                                            count++;
                                        }
                                    }

                                    changeValue = count > 1;
                                }

                                if (changeValue)
                                {
                                    module.intValue = (int)currentModules[j];
                                }
                            }
                        }
                        GUILayout.FlexibleSpace();
                        GUI.color = Color.white;
                    }

                GUILayout.EndVertical();
            }

        GUILayout.EndHorizontal();

        if (missingModules.Count > 0 && !missingModules.Contains(ModuleType.Blank))
        {
            // Drop down list to add missing modules
            var dropDownList = new List<string>();
            dropDownList.Add("Add Module");
            for (int i = 0; i < missingModules.Count; i++)
            {
                dropDownList.Add(missingModules[i].ToString());
            }

            var index = 0;
            index = EditorGUILayout.Popup(index, dropDownList.ToArray(), dropDownButtonStyle, GUILayout.Height(RowHeight));
            if (index > 0)
            {
                for (int i = 0; i < modules.arraySize; i++)
                {
                    var module = modules.GetArrayElementAtIndex(i);

                    if ((ModuleType)module.intValue == ModuleType.Blank)
                    {
                        module.intValue = (int)missingModules[index - 1];
                        break;
                    }
                }
            }
        }

        GUILayout.Space(RowHeight);

        // For setting power requirement (if at least 1 power module exists)
        var requiresPower = false;
        var powerSP = serializedObject.FindProperty("powerRequirement");

        for (int i = 0; i < modules.arraySize; i++)
        {
            var module = (ModuleType)modules.GetArrayElementAtIndex(i).enumValueIndex;
            if (module == ModuleType.PowerInput)
            {
                EditorGUILayout.PropertyField(powerSP, true);
                requiresPower = true;

                break;
            }
        }

        if (!requiresPower)
        {
            var relColorSP = powerSP.FindPropertyRelative("color");
            relColorSP.enumValueIndex = (int)LaserColor.Black;

            var relPowerSP = powerSP.FindPropertyRelative("power");
            relPowerSP.intValue = 0;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
