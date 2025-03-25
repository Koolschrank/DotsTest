using Unity.Entities;
using UnityEngine;

public class MoveForwardAuthoring : MonoBehaviour
{
    public float speed = 2;

    public class Baker : Baker<MoveForwardAuthoring>
    {
        public override void Bake(MoveForwardAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new MoveForwardComponent
            {
                speed = authoring.speed
            });
        }
    }

}

public struct MoveForwardComponent : IComponentData
{
    public float speed;
}
