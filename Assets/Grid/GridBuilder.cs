using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GridBuilder : MonoBehaviour
{
    [SerializeField] int blockCount = 10;
    [SerializeField] Vector2 startPos;
    [SerializeField] float lenght;



    public void Awake()
    {
        CollisionSystem.SetUpCollisionGrid(blockCount, startPos, lenght);
    }



    // gizmo
    void OnDrawGizmos()
    {

        List<Vector2Int> hitIndex = new List<Vector2Int>();
        List<Vector2Int> occupiedIndex = CollisionSystem.GetAllOccupiedBlocks();

        var lenghtOfBlock = lenght / blockCount;
        for (int i = 0; i < blockCount; i++)
        {
            for (int j = 0; j < blockCount; j++)
            {
                


                Vector3 position = new Vector3(startPos.x + i * lenghtOfBlock, 0, startPos.y + j * lenghtOfBlock);
                Vector3 offset = new Vector3(1,0,1) * lenghtOfBlock / 2;

                if (hitIndex.Contains(new Vector2Int(i, j)))
                {
                    Gizmos.color = Color.red;

                    offset += Vector3.up * 0.2f;
                }
                else if (occupiedIndex.Contains(new Vector2Int(i, j)))
                {
                    Gizmos.color = Color.yellow;
                    offset += Vector3.up * 0.2f;
                }
                else
                {
                    Gizmos.color = Color.blue;
                }

                Gizmos.DrawWireCube(position + new Vector3(offset.x, offset.y, offset.z), new Vector3(lenghtOfBlock, 0, lenghtOfBlock));
            }
        }
    }
}
