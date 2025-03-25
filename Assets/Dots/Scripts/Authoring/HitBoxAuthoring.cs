using Unity.Entities;
using UnityEngine;

public class HitBoxAuthoring : MonoBehaviour
{
    public float size = 2;
    public int hitLayer = 0;
    public int damage = 1;


    public class Baker : Baker<HitBoxAuthoring>
    {
        public override void Bake(HitBoxAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new HitBoxComponent
            {
                size = authoring.size,
                hitLayer = authoring.hitLayer,
                damage = authoring.damage
            });
        }
    }

}

public struct HitBoxComponent : IComponentData
{
    public float size;
    public int hitLayer;
    public int damage;
}