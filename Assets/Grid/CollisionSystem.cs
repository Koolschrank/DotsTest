using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


public static class CollisionSystem
{
    static List<List<CollisionBlock>> collisionBlocks = new List<List<CollisionBlock>>();
    static Vector2 startingVector = new Vector2(0, 0);
    static float lenght = 1;

    public static void SetUpCollisionGrid(int listSize, Vector2 startingVector, float lenght )
    {
        collisionBlocks = new List<List<CollisionBlock>>();
        for (int i = 0; i < listSize; i++)
        {
            collisionBlocks.Add(new List<CollisionBlock>());
            for (int j = 0; j < listSize; j++)
            {
                collisionBlocks[i].Add(new CollisionBlock());
            }
        }
        CollisionSystem.startingVector = startingVector;
        CollisionSystem.lenght = lenght / listSize;
    }

    public static CollisionBlock GetCollisionBlock(Vector3 position)
    {
        int x = (int)((position.x - startingVector.x) / lenght);
        int y = (int)((position.z - startingVector.y) / lenght);
        return collisionBlocks[x][y];
    }

    public static Vector2Int GetIndex(Vector3 position)
    {
        int x = (int)((position.x - startingVector.x) / lenght);
        int y = (int)((position.z - startingVector.y) / lenght);
        return new Vector2Int(x, y);
    }

    public static List<Vector2Int> GetIndexList(Vector3 startPos, Vector3 endPos)
    {
        int xStart = (int)((startPos.x - startingVector.x) / lenght);
        int yStart = (int)((startPos.z - startingVector.y) / lenght);
        int xEnd = (int)((endPos.x - startingVector.x) / lenght);
        int yEnd = (int)((endPos.z - startingVector.y) / lenght);

        List<Vector2Int> indexes = new List<Vector2Int>();
        for (int i = xStart; i <= xEnd; i++)
        {
            for (int j = yStart; j <= yEnd; j++)
            {
                indexes.Add(new Vector2Int(i, j));
            }
        }

        return indexes;
    }

    public static List<CollisionBlock> GetCollisionBlocks(Vector3 startPos, Vector3 endPos)
    {
        List<CollisionBlock> blocks = new List<CollisionBlock>();
        int xStart = (int)((startPos.x - startingVector.x) / lenght);
        int yStart = (int)((startPos.z - startingVector.y) / lenght);
        int xEnd = (int)((endPos.x - startingVector.x) / lenght);
        int yEnd = (int)((endPos.z - startingVector.y) / lenght);

        for (int i = xStart; i <= xEnd; i++)
        {
            for (int j = yStart; j <= yEnd; j++)
            {
                blocks.Add(collisionBlocks[i][j]);
            }
        }

        return blocks;
    }

    public static void MoveObject(HurtBox hurtBox, List<Vector2Int> oldPositions, List<Vector2Int> newPositions)
    {
        List<Vector2Int> toRemove = new List<Vector2Int>();
        List<Vector2Int> toAdd = new List<Vector2Int>();

        foreach (Vector2Int position in oldPositions)
        {
            if (!newPositions.Contains(position))
            {
                toRemove.Add(position);
            }
        }

        foreach (Vector2Int position in newPositions)
        {
            if (!oldPositions.Contains(position))
            {
                toAdd.Add(position);
            }
        }

        RemoveObject(hurtBox, toRemove);
        AddObject(hurtBox, toAdd);
    }

    public static void RemoveObject(HurtBox hurtBox, List<Vector2Int> positions)
    {
        foreach (Vector2Int position in positions)
        {
            collisionBlocks[position.x][position.y].RemoveHurtBox(hurtBox);
        }
    }

    public static void AddObject(HurtBox hurtBox, List<Vector2Int> positions)
    {
        foreach (Vector2Int position in positions)
        {
            collisionBlocks[position.x][position.y].AddHurtBox(hurtBox);
        }
    }

    public static List<HurtBox> GetAllObjectsFromList(List<Vector2Int> positions)
    {
        List<HurtBox> hurtBoxes = new List<HurtBox>();
        foreach (Vector2Int position in positions)
        {
            hurtBoxes.AddRange(collisionBlocks[position.x][position.y].GetHurtBoxes());
        }
        return hurtBoxes;
    }

    public static bool CheckIfCollisonsAreInGrid(List<Vector2Int> positions)
    {
        foreach (Vector2Int position in positions)
        {
            if (position.x < 0 || position.x >= collisionBlocks.Count || position.y < 0 || position.y >= collisionBlocks.Count)
            {
                return false;
            }
        }
        return true;
    }

    public static List<Vector2Int> GetAllOccupiedBlocks()
    {
        List<Vector2Int> indexes = new List<Vector2Int>();
        for (int i = 0; i < collisionBlocks.Count; i++)
        {
            for (int j = 0; j < collisionBlocks[i].Count; j++)
            {
                if (collisionBlocks[i][j].GetHurtBoxes().Count > 0)
                {
                    indexes.Add(new Vector2Int(i, j));
                }
            }
        }
        return indexes;
    }


}


public class CollisionBlock
{

    List<HurtBox> hurtBoxes = new List<HurtBox>();

    public void AddHurtBox(HurtBox hurtBox)
    {
        hurtBoxes.Add(hurtBox);
    }

    public void RemoveHurtBox(HurtBox hurtBox)
    {
        hurtBoxes.Remove(hurtBox);
    }

    public List<HurtBox> GetHurtBoxes()
    {
        return hurtBoxes;
    }

    public bool ContainsHurtBox(HurtBox hurtBox)
    {
        return hurtBoxes.Contains(hurtBox);
    }






}

