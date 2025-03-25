using Unity.Entities;
using UnityEngine;

public class PlayerMovementAuthoring : MonoBehaviour
{
    public float moveSpeed = 2;


    public class Baker : Baker<PlayerMovementAuthoring>
    {
        public override void Bake(PlayerMovementAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerMovementComponent
            {
                moveSpeed = authoring.moveSpeed

            });
        }
    }
}


public struct PlayerMovementComponent : IComponentData
{
    public float moveSpeed;
}
