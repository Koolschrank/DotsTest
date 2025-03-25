using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

partial struct EnemySpawnerSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (
            (var localTransform,
            var spawner)
            in SystemAPI.Query<RefRO<LocalTransform>, RefRW<EnemySpawnerComponent>>())
        {

            int count = 0;  
            spawner.ValueRW.spawnTimer -= SystemAPI.Time.DeltaTime;
            while(spawner.ValueRO.spawnTimer < 0)
            {
                count ++;
                spawner.ValueRW.spawnTimer += spawner.ValueRO.spawnRate;
            }

            if (count == 0) return;

            var random = spawner.ValueRO.random;
            for (int i = 0; i < count; i++)
            {
                float2 randomPoint = random.NextFloat2Direction() * spawner.ValueRO.spawnRadius;
                float3 position = new float3(randomPoint.x, 0, randomPoint.y) + localTransform.ValueRO.Position;

                Entity spawnedEntity = state.EntityManager.Instantiate(spawner.ValueRO.entityToSpawn);
                SystemAPI.SetComponent(spawnedEntity, LocalTransform.FromPosition(position));
                var entityTransform = SystemAPI.GetComponentRW<LocalTransform>(spawnedEntity);
            }
            spawner.ValueRW.random = random;
        }

    }


}