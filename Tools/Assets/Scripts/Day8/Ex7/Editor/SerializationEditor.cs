using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SerializationScript))]
public class SerializationEditor : Editor
{
    SerializationScript serialization;
    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("write Json"))
        {
            SerializationScript hero = new SerializationScript();
            string myJson = JsonUtility.ToJson(hero);
            Debug.Log(myJson);
            Debug.Log(Application.streamingAssetsPath);
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }
            File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "transforms.json"), myJson);
        }
        if (GUILayout.Button("read Json"))
        {
            if (Directory.Exists(Application.streamingAssetsPath))
            {
                string data = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "transforms.json"));
                SerializationScript hero = JsonUtility.FromJson<SerializationScript>(data);
                Debug.Log(hero.ToString());
            }
        }
        if (GUILayout.Button("write Json List"))
        {
            List<SerializationScript> list2 = new List<SerializationScript>();
            for (int i = 0; i < 10; i++)
            {
                list2.Add(new SerializationScript());
            }

            // Debug.Log(list2[2]);
            string listString = JsonUtility.ToJson(new HeroWrraper(list2));
            Debug.Log(listString);
            Debug.Log(Application.streamingAssetsPath);
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }
            File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "transform.json"), listString);
        }
        if (GUILayout.Button("read Json list"))
        {
            if (Directory.Exists(Application.streamingAssetsPath))
            {
                string data = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "transform.json"));
                HeroWrraper hero = JsonUtility.FromJson<HeroWrraper>(data);
                foreach (SerializationScript hero1 in hero.list)
                {
                    Debug.Log(hero1.ToString());
                }
            }
        }
    }

    void OnSceneGUI()
    {
        
    }
    class HeroWrraper
    {
        public List<SerializationScript> list;
        public HeroWrraper(List<SerializationScript> list)
        {
            this.list = list;
        }
    }
}
