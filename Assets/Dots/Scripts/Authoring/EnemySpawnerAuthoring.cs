using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class EnemySpawnerAuthoring : MonoBehaviour
{
    public GameObject entityToSpawn;
    public float spawnRate = 1.0f;
    public float spawnRadius = 1.0f;


    public class Baker : Baker<EnemySpawnerAuthoring>
    {
        public override void Bake(EnemySpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemySpawnerComponent
            {
                spawnRate = authoring.spawnRate,
                spawnTimer = authoring.spawnRate,
                spawnRadius = authoring.spawnRadius,
                entityToSpawn = GetEntity(authoring.entityToSpawn, TransformUsageFlags.Dynamic),
                random = new Unity.Mathematics.Random(1)
            });
        }
    }

}

public struct EnemySpawnerComponent : IComponentData
{
    public float spawnRate;
    public float spawnTimer;
    public float spawnRadius;
    public Entity entityToSpawn;
    public Unity.Mathematics.Random random;

}