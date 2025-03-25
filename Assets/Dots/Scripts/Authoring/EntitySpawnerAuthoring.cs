using Unity.Entities;
using UnityEngine;

public class EntitySpawnerAuthoring : MonoBehaviour
{
    public GameObject entityToSpawn;
    public float timer = 2;
    public int count = 1;
    public float anglePerObject = 10.0f;




    public class Baker : Baker<EntitySpawnerAuthoring>
    {
        public override void Bake(EntitySpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EntitySpawnerComponent
            {
                timer = authoring.timer,
                timerMax = authoring.timer,
                entityToSpawn = GetEntity(authoring.entityToSpawn, TransformUsageFlags.Dynamic),
                count = authoring.count,
                anglePerObject = authoring.anglePerObject
            });
        }
    }

}

public struct EntitySpawnerComponent : IComponentData
{
    public float timer;
    public float timerMax;

    public Entity entityToSpawn;
    public int count;
    public float anglePerObject;
}