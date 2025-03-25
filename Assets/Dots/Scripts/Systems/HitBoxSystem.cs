using Unity.Burst;
using Unity.Entities;
using static UnityEngine.GraphicsBuffer;
using Unity.Transforms;
using Unity.Collections;
using Unity.Physics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

partial struct HitBoxSystem : ISystem
{


    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer entityCommandBuffer = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

        PhysicsWorldSingleton physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
        CollisionWorld collisionWorld = physicsWorldSingleton.CollisionWorld;
        NativeList<DistanceHit> distanceHitList = new NativeList<DistanceHit>(Allocator.Temp);

        foreach (
            (var localTransform,
            var hitBox,
            var entity)
            in SystemAPI.Query<RefRO<LocalTransform>, RefRW<HitBoxComponent>>().WithEntityAccess())
        {
            distanceHitList.Clear();
            CollisionFilter filter = new CollisionFilter
            {
                BelongsTo = ~0u,
                CollidesWith = 1u << hitBox.ValueRO.hitLayer,
                GroupIndex = 0
            };

            if (collisionWorld.OverlapSphere(localTransform.ValueRO.Position, hitBox.ValueRO.size, ref distanceHitList, filter))
            {
                foreach (var distanceHit in distanceHitList)
                {

                    var targetEntity = distanceHit.Entity;
                    if (SystemAPI.HasComponent<HealthComponent>(targetEntity))
                    {
                        RefRW<HealthComponent> targetUnit = SystemAPI.GetComponentRW<HealthComponent>(targetEntity);
                        if (targetUnit.ValueRO.health > 0)
                        {
                            targetUnit.ValueRW.health -= hitBox.ValueRO.damage;
                            entityCommandBuffer.DestroyEntity(entity);
                        }
                    }
                        

                    
                }
            }
        }
    }

}