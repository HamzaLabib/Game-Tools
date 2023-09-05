using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEditor;

[Serializable]
public class SerializationScript : MonoBehaviour
{
    string[] prefabPath;
    public Rigidbody prefab;
    List<Rigidbody> prefabs;
    MeshRenderer rend;
    Vector3 speed = new Vector3(5,0,5);

    void Awake()
    {
        prefabPath = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets/Prefabs"});
        //prefab = AssetDatabase.LoadAssetAtPath<Transform>(prefabPath[3]);
        prefabs = new List<Rigidbody>();
    }

    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            Rigidbody toClone = Instantiate(prefab, new Vector3(UnityEngine.Random.Range(-50, 50), 2, UnityEngine.Random.Range(-50, 50))
                ,Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), 0,0));
            prefabs.Add(toClone);
            rend = toClone.GetComponent<MeshRenderer>();
            rend.material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f,1f), UnityEngine.Random.Range(0f, 1f),1);
        }
    }

    void Update()
    {
        foreach (Rigidbody t in prefabs)
            AutoMovement(t, speed);
        
    }

    void AutoMovement(Rigidbody gameObject, Vector3 speed)
    {
        gameObject.velocity += speed * Time.deltaTime;

        if (gameObject.position.x > 50 || gameObject.position.x < -50)
            gameObject.velocity *= -1;
        if (gameObject.position.z > 50 || gameObject.position.z < -50)
            gameObject.velocity *= -1;
    }
}
