using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class WalkToTargetAuthoring : MonoBehaviour
{
    public float moveSpeed = 1;


    public class Baker : Baker<WalkToTargetAuthoring>
    {
        public override void Bake(WalkToTargetAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new WalkToTargetComponent
            {
                moveSpeed = authoring.moveSpeed,
                target = new float3(0, 0, 0)
            });
        }
    }

}

public struct WalkToTargetComponent : IComponentData
{
    public float moveSpeed;
    public float3 target;
}