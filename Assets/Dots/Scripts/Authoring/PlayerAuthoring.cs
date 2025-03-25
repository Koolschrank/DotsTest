using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public class Baker : Baker<PlayerMovementAuthoring>
    {
        public override void Bake(PlayerMovementAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerComponent());
        }
    }
}


public struct PlayerComponent : IComponentData
{
}
