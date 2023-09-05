using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.IO;

public class PlacerTool : EditorWindow
{
    string[] pathIdentifier;
    List<string> pathString;
    List<GameObject> prefabs;
    int selectValue;

    public static EditorWindow placerWindow;

    private void OnEnable()
    {
        pathIdentifier = AssetDatabase.FindAssets("t:Object", new string[] { "Assets/Prefabs" });
    }

    [MenuItem("My Tools/PlacerTool")]
    public static void CreateWindow()
    {
        placerWindow = GetWindow<PlacerTool>("Placer Tool");
    }

    public void OnGUI()
    {
        NULLCheck();
        EditorGUILayout.BeginVertical();
            WindowCreation();
        EditorGUILayout.EndVertical();
    }

    void WindowCreation()
    {
        EditorGUILayout.LabelField("Object Placer", EditorStyles.boldLabel);
        PopupCreation();
        SwapButtonHandle();
    }

    void NULLCheck()
    {

        if (pathString == null)
            pathString = new List<string>();
        if (prefabs == null)
            prefabs = new List<GameObject>();

    }

    void PopupCreation()
    {
        foreach (var path in pathIdentifier)
        {
            //convert from guid to string path "Identifier"
            string thePath = AssetDatabase.GUIDToAssetPath(path);
            pathString.Add(Path.GetFileNameWithoutExtension(thePath));
            prefabs.Add(AssetDatabase.LoadAssetAtPath<GameObject>(thePath));
        }

        selectValue = EditorGUILayout.Popup(selectValue, pathString.ToArray());
    }

    void SwapButtonHandle()
    {
        if (GUILayout.Button("Swap"))
        {
            for (int i = 0; i < prefabs.Count; i++)
            {

                if (selectValue == i)
                {
                    Instantiate(prefabs[i], new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10)), Quaternion.Euler(0, 0, 0));
                    break;
                }
            }
        }
    }
}
