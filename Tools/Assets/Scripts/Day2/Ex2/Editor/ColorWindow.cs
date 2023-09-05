using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorWindow : EditorWindow
{
    int rowNumber = 5;
    int colNumber = 5;
    int maxRow;
    int maxCol;

    MeshRenderer renderer;
    Color matColor = Color.white;
    Color eraseColor = Color.white;
    List<List<Color>> colors;
    Texture colorTexture;

    public static EditorWindow colorWindow;

    private void OnEnable()
    {
        maxRow = rowNumber;
        maxCol = colNumber;
        colors = CreateNewList(maxRow, maxCol);
        colorTexture = EditorGUIUtility.whiteTexture;
    }

    private List<List<Color>> CreateNewList(int _rowNumber, int _colNumber)
    {
        List<List<Color>> colors = new List<List<Color>>();
        for (int i = 0; i < _rowNumber; i++)
        {
            colors.Add(new List<Color>());
            for (int j = 0; j < _colNumber; j++)
            {
                colors[i].Add(Color.white);
            }
        }
        return colors;
    }

    [MenuItem("My Tools/ColorMenu")]
    public static void CreateWindow()
    {
        colorWindow = GetWindow<ColorWindow>("Color Editor");
    }

    public void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
                LeftSide();
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
                RightSide();
            EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        CreateNewList(maxRow, maxCol);
    }

    void LeftSide()
    {
        EditorGUILayout.BeginHorizontal();

        ChechIfRowColHasChanged();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("ToolBar", EditorStyles.boldLabel);

        matColor = EditorGUILayout.ColorField("Paint Color", matColor);
        eraseColor = EditorGUILayout.ColorField("Erase Color", eraseColor);
        if (GUILayout.Button("Fill All"))
        {
            for (int i = 0; i < colors.Count; i++)
            {
                for (int j = 0; j < colors[i].Count; j++)
                {
                    colors[i][j] = matColor;
                }
            }
        }

        renderer = EditorGUILayout.ObjectField("Output Renderer", renderer, typeof(MeshRenderer), true) as MeshRenderer;
        if (GUILayout.Button("Save to Object"))
        {
            ChangeMaterial();
        }
    }

    private void ChechIfRowColHasChanged()
    {
        int maxRowNew = EditorGUILayout.IntField("# Rows", maxRow);
        int maxColNew = EditorGUILayout.IntField("# Col", maxCol);

        if (maxRowNew != maxRow || maxColNew != maxCol)
        {
            maxRow = maxRowNew;
            maxCol = maxColNew;
            colors = CreateNewList(maxRow, maxCol);
        }
    }

    void RightSide()
    {
        Rect rect;
        for (int row = 0; row < maxRow; row++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int col = 0; col < maxCol; col++)
            {
                GUI.color = colors[row][col];
                rect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true),
                                GUILayout.ExpandHeight(true));
                GUI.DrawTexture(rect, colorTexture);
                RectSetColor(rect, row, col);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    public void RectSetColor(Rect rect, int i, int j)
    {
        Event e = Event.current;

        if (rect.Contains(e.mousePosition) && e.type == EventType.MouseDrag)
        {

            if (e.button == 0)
            {
                colors[i][j] = matColor;
            }
            if (e.button == 1)
            {
                colors[i][j] = eraseColor;

            }
            e.Use();
        }
    }

    void ChangeMaterial()
    {
        //Create a new texture
        Texture2D texture2D = new Texture2D(maxRow, maxCol); 
        //Simplest non-blend texture mode
        texture2D.filterMode = FilterMode.Point; 
        //Materials require Shaders as an arguement, Diffuse is the most basic type
        renderer.material = new Material(Shader.Find("Diffuse")); 
        //sharedMaterial is the MAIN RESOURCE MATERIAL. Changing this will change ALL objects using it, .material will give you the local instance
        renderer.sharedMaterial.mainTexture = texture2D;

        for (int i = 0; i < maxRow; i++)
        {
            for (int j = 0; j < maxCol; j++)
            {
                //Color every pixel using our color table, the texture is 8x8 pixels large, but strecthes to fit
                texture2D.SetPixel(i, maxCol - 1 - j, colors[i][j]);
            }
        }
        texture2D.Apply();
    }
}
