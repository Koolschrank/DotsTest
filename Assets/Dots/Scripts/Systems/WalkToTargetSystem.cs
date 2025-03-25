using System.Numerics;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

partial struct WalkToTargetSystem : ISystem
{


    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {


        // not the best way to get the player but certainly a way to get the player
        float3 targetPosition = new float3(0, 0, 0);
        foreach (
            (var localTransform,
            var playerMovement)
            in SystemAPI.Query<RefRO<LocalTransform>, RefRO<PlayerComponent>>())
        {
            targetPosition = localTransform.ValueRO.Position;
        }


        WalkToTargetJob walkTargetJob = new WalkToTargetJob
        {
            deltaTime = SystemAPI.Time.DeltaTime,
            target = targetPosition
        };

        walkTargetJob.ScheduleParallel();

    }
}

[BurstCompile]
public partial struct WalkToTargetJob : IJobEntity
{
    public float deltaTime;
    public float3 target;

    public void Execute(ref LocalTransform localTransform, ref WalkToTargetComponent walkToTarget)
    {
        walkToTarget.target = target;

        float3 direction = walkToTarget.target - localTransform.Position;
        float distance = math.length(direction);
        if (distance > 0.1f)
        {
            float3 move = math.normalize(direction) * walkToTarget.moveSpeed * deltaTime;
            localTransform.Position += move;
        }
    }
}