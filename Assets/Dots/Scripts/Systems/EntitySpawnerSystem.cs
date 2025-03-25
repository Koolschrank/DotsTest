using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

partial struct EntitySpawnerSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        
        foreach (
            (var localTransform,
            var spawner)
            in SystemAPI.Query<RefRO<LocalTransform>, RefRW<EntitySpawnerComponent>>())
        {
            spawner.ValueRW.timer -= SystemAPI.Time.DeltaTime;
            if (spawner.ValueRO.timer >0)
            {
                continue;
            }
            spawner.ValueRW.timer = spawner.ValueRO.timerMax;


            Quaternion rotation = localTransform.ValueRO.Rotation;
            for (int i = 0; i < spawner.ValueRO.count; i++)
            {

                Entity spawnedEntity = state.EntityManager.Instantiate(spawner.ValueRO.entityToSpawn);

                var entityTransform = SystemAPI.GetComponentRW<LocalTransform>(spawnedEntity);
                entityTransform.ValueRW.Position = localTransform.ValueRO.Position;
                entityTransform.ValueRW.Rotation = rotation;

                rotation *= Quaternion.Euler(0, spawner.ValueRO.anglePerObject, 0);
            }
        }
    }

}

