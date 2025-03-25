using Unity.Burst;
using Unity.Entities;

[UpdateInGroup(typeof(LateSimulationSystemGroup))]
partial struct HealthDeadSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {

        EntityCommandBuffer entityCommandBuffer = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
        
        foreach (
            (var health,
            Entity entity)
            in SystemAPI.Query<RefRO<HealthComponent>>().WithEntityAccess())
        {
            if (health.ValueRO.health <= 0)
            {
                entityCommandBuffer.DestroyEntity(entity); // ques the command to destroy the entity
            }
        }
    }
}
