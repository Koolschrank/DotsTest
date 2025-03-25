using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    List<Vector2Int> occupiedBlocks = new List<Vector2Int>();
    [SerializeField] float radius = 0.5f;
    

    public float Radius { get => radius; }

    [SerializeField] int updateRate = 1;
    int updateIndex = 0;
    public void OnEnable()
    {
        updateIndex = Random.Range(0, updateRate);

    }

    void Update()
    {
        if (Time.frameCount % updateRate != updateIndex)
        {
            return;
        }

        Vector3 position = transform.position;
        Vector3 offset = Vector3.one * radius;

        List<Vector2Int> newOccupiedBlocks = CollisionSystem.GetIndexList(position - offset, position + offset);

        bool hasChanged = false;
        if (occupiedBlocks.Count != newOccupiedBlocks.Count)
        {
            hasChanged = true;
        }
        else
        {
            for (int i = 0; i < occupiedBlocks.Count; i++)
            {
                if (occupiedBlocks[i] != newOccupiedBlocks[i])
                {
                    hasChanged = true;
                    break;
                }
            }
        }

        if (hasChanged)
        {
            CollisionSystem.MoveObject(this, occupiedBlocks, newOccupiedBlocks);
            occupiedBlocks = newOccupiedBlocks;
        }
    }


    // gizmo
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OnDisable()
    {
        CollisionSystem.RemoveObject(this, occupiedBlocks);
    }
}
