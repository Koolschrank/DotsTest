using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

partial struct RotateSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // job
        RotateJob rotateJob = new RotateJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        rotateJob.ScheduleParallel();


       
    }

}

// job
[BurstCompile]
public partial struct RotateJob : IJobEntity
{
    public float deltaTime;
    public void Execute(ref LocalTransform localTransform, in RotateComponent rotate)
    {
        float angle = rotate.speed * deltaTime;
        Quaternion rotation = Quaternion.AngleAxis(angle, math.up());
        localTransform.Rotation = math.mul(localTransform.Rotation, rotation);
    }
}
