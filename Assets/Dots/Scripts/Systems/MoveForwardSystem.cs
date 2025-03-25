using Unity.Burst;
using Unity.Entities;
using static UnityEngine.GraphicsBuffer;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Physics;

partial struct MoveForwardSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        MoveForwardJob moveForwardJob = new MoveForwardJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };

        moveForwardJob.ScheduleParallel();

    }


}

[BurstCompile]
public partial struct MoveForwardJob : IJobEntity
{

    public float deltaTime;
    public void Execute(ref LocalTransform localTransform, in MoveForwardComponent moveForward)
    {
        localTransform.Position += localTransform.Forward() * moveForward.speed * deltaTime;
    }
}