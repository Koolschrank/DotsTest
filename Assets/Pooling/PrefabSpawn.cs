using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PrefabSpawn
{
    // prefab to spawn
    [SerializeField] GameObject prefab;


    public GameObject SpawnObject()
    {
        return ObjectPoolManager.SpawnObject(prefab);
    }

    public GameObject SpawnObject(Vector3 position)
    {
        if (prefab == null)
        {
            return null;
        }
        GameObject obj = SpawnObject();
        obj.transform.position = position;
        return obj;
    }

    public GameObject SpawnObject(Vector3 position, Quaternion rotation)
    {
        if (prefab == null)
        {
            return null;
        }
        GameObject obj = SpawnObject();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }



    public GameObject SpawnObject(Transform origin)
    {
        if (prefab == null)
        {
            return null;
        }
        GameObject obj = SpawnObject();
        obj.transform.position = origin.position;
        obj.transform.rotation = origin.rotation;
        return obj;
    }

    public GameObject SpawnObject(Transform origin, Vector3 position, Quaternion rotation)
    {
        if (prefab == null)
        {
            return null;
        }
        GameObject obj = SpawnObject();
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        return obj;

    }

    public GameObject Prefab
    {
        get { return prefab; }
    }

}