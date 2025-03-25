using Unity.Entities;
using UnityEngine;

public class HealthAuthoring : MonoBehaviour
{
    public int health = 1;


    public class Baker : Baker<HealthAuthoring>
    {
        public override void Bake(HealthAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new HealthComponent
            {
                health = authoring.health,
                healthMax = authoring.health
            });
        }
    }

}

public struct HealthComponent : IComponentData
{
    public int health;
    public int healthMax;
}