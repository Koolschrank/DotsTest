using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] PrefabSpawn enemy;
    [SerializeField] float spawnRate = 1.0f;
    float nextSpawnTime = 0.0f;

    [SerializeField] float spawnRadius = 1.0f;


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnRadius;
            spawnDirection = new Vector3(spawnDirection.x, 0, spawnDirection.y);

            var obj =enemy.SpawnObject(transform);
            
            obj.transform.position = spawnDirection;

            nextSpawnTime = Time.time + spawnRate;
        }
    }
}
