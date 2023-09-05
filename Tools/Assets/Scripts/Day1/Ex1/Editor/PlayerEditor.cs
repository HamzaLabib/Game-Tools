using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerMovement))]
public class PlayerEditor : Editor
{
    PlayerMovement player;
    public List<Transform> waypoints;

    private void OnEnable()
    {
        player = (PlayerMovement)target;
        waypoints = player.waypoints;
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        EditorGUILayout.LabelField("Movement Controller Params", EditorStyles.boldLabel);

        player.speed = EditorGUILayout.FloatField("Speed", player.speed);

        EditorGUILayout.LabelField("Waypoints", EditorStyles.boldLabel);
        HorizontalLines();
        DropAreaGUI();
    }

    private void OnSceneGUI()
    {
        DrawLabelInScene();
    }

    public void HorizontalLines()
    {
        if (waypoints.Count > 0)
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                waypoints[i] = EditorGUILayout.ObjectField(waypoints[i], typeof(Transform), true) as Transform;

                if (GUILayout.Button("^"))
                {
                    if (i != 0)
                    {
                        Transform temp;
                        temp = waypoints[i - 1];
                        waypoints[i - 1] = waypoints[i];
                        waypoints[i] = temp;
                    }
                }

                if (GUILayout.Button("v"))
                {
                    if (i != (waypoints.Count - 1))
                    {
                        Transform temp;
                        temp = waypoints[i + 1];
                        waypoints[i + 1] = waypoints[i];
                        waypoints[i] = temp;
                    }
                }

                if (GUILayout.Button("1st"))
                {
                    if (i != 0)
                    {
                        Transform temp;
                        temp = waypoints[0];
                        waypoints[0] = waypoints[i];
                        waypoints[i] = temp;
                    }
                }

                if (GUILayout.Button("end"))
                {
                    if (i != (waypoints.Count - 1))
                    {
                        Transform temp;
                        temp = waypoints[waypoints.Count - 1];
                        waypoints[waypoints.Count - 1] = waypoints[i];
                        waypoints[i] = temp;
                    }
                }

                if (GUILayout.Button("-"))
                {
                    waypoints.Remove(waypoints[i]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }

    public void DrawLabelInScene()
    {

        for (int i = 0; i < waypoints.Count; i++)
        {
            Handles.BeginGUI();
            Handles.color = Color.red;
            Handles.Label(waypoints[i].position, "Waypoint " + i);
            Handles.EndGUI();
        }
    }

    public void DropAreaGUI()
    {
        Event e = Event.current;
        Rect dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Add waypoint");

        switch (e.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(e.mousePosition))
                    return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (e.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
                    {
                        GameObject go = DragAndDrop.objectReferences[i] as GameObject;
                        player.waypoints.Add(go.transform);
                    }
                }
                break;
        }
    }
}
