using Unity.Entities;
using UnityEngine;

public class RotateAuthoring : MonoBehaviour
{
    public float speed = 2;

    public class Baker : Baker<RotateAuthoring>
    {
        public override void Bake(RotateAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotateComponent
            {
                speed = authoring.speed
            });
        }
    }

}

public struct RotateComponent : IComponentData
{
    public float speed;
}