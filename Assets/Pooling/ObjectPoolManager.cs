using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> PooledObjects = new List<PooledObjectInfo>();


    public static GameObject SpawnObject(GameObject objectToSpawn)
    {
        PooledObjectInfo pooledObjectInfo = PooledObjects.Find(x => x.LookupString == objectToSpawn.name);
        if (pooledObjectInfo == null)
        {
            pooledObjectInfo = new PooledObjectInfo();
            pooledObjectInfo.LookupString = objectToSpawn.name;
            PooledObjects.Add(pooledObjectInfo);
        }

        GameObject objectToReturn = null;
        foreach (GameObject inactiveObject in pooledObjectInfo.InactiveObjects)
        {
            if (inactiveObject != null)
            {
                objectToReturn = inactiveObject;
                break;
            }
        }

        if (objectToReturn == null)
        {
            objectToReturn = Instantiate(objectToSpawn, Vector3.one * 1000, Quaternion.identity);
            // debug log
            //Debug.Log("Instantiated new object: " + objectToReturn.name);
        }
        else
        {
            objectToReturn.SetActive(true);
            pooledObjectInfo.InactiveObjects.Remove(objectToReturn);
            // debug log
            //Debug.Log("Reused object: " + objectToReturn.name);
        }
        return objectToReturn;
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        var objectToReturn = SpawnObject(objectToSpawn);
        objectToReturn.transform.position = spawnPosition;
        objectToReturn.transform.rotation = spawnRotation;
        return objectToReturn;
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Transform parent)
    {
        var objectToReturn = SpawnObject(objectToSpawn);
        objectToReturn.transform.SetParent(parent);

        return objectToReturn;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string objName = obj.name.Substring(0, obj.name.Length - 7);

        PooledObjectInfo pooledObjectInfo = PooledObjects.Find(x => x.LookupString == objName);

        if (pooledObjectInfo == null)
        {
            // debug warning
            Debug.LogWarning("No pooled object info found for object: " + objName);
        }
        else
        {
            obj.SetActive(false);
            pooledObjectInfo.InactiveObjects.Add(obj);
        }
    }

}

public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
