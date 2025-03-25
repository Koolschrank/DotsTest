using UnityEngine;
using System.Collections.Generic;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] PrefabSpawn prefabSpawn;

    [SerializeField] float fireRate = 1.0f;
    float cooldown = 0.0f;

    [SerializeField] int count = 1;
    [SerializeField] float angle = 10.0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (prefabSpawn != null) {
            if (cooldown <= 0.0f)
            {
                Quaternion rotation = transform.rotation;
                for (int i = 0; i < count; i++)
                {

                    prefabSpawn.SpawnObject(transform.position, rotation);
                    rotation *= Quaternion.Euler(0, angle, 0);



                }


                
                cooldown = fireRate;
            }
            cooldown -= Time.deltaTime;
        }

    }
}
