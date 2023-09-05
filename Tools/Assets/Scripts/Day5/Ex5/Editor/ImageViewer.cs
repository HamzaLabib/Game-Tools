using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ImageViewer : EditorWindow
{
    string[] foldersPathString;
    string[] texturesPath;
    List<Texture2D> texture2Ds;

    int toolbarInt = 0;

    public static EditorWindow imageViewer;

    private void OnEnable()
    {
        texture2Ds = new List<Texture2D>();
        foldersPathString = AssetDatabase.GetSubFolders("Assets/Images");
    }

    [MenuItem("My Tools/ImageEditor")]
    public static void CreateWindow()
    {
        imageViewer = GetWindow<ImageViewer>("Image Editor");
    }

    public void OnGUI()
    {
        WindowCreation();
    }

    void WindowCreation()
    {
        EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Images Folders", EditorStyles.boldLabel);
            toolbarInt = GUILayout.Toolbar(toolbarInt, foldersPathString);
            ToolbarHandle();
        EditorGUILayout.EndVertical();
    }

    void ToolbarHandle()
    {
        EditorGUILayout.BeginHorizontal();
            GetAllTextures(toolbarInt);
            foreach (Texture2D image in texture2Ds)
            {
                GUILayout.Box(image, GUILayout.Width(200), GUILayout.Height(200));
            }
        EditorGUILayout.EndHorizontal();
    }

    void GetAllTextures(int toolbarInt)
    {
        texture2Ds.Clear();
        texturesPath = AssetDatabase.FindAssets("t:Texture2D", new string[] { foldersPathString[toolbarInt] });
        for (int i = 0; i < texturesPath.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(texturesPath[i]);
            Texture2D t = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            texture2Ds.Add(t);
            //Debug.Log(thePath);
        }
    }
}
