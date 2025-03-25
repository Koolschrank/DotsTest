using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private void OnDisable()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
