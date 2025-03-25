using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


public class HitBox : MonoBehaviour
{
    [SerializeField] float radius = 0.5f;

    List<HurtBox> pastHurtBoxes = new List<HurtBox>();

    public float Radius { get => radius; }

    [SerializeField] int updateRate = 1;
    int updateIndex = 0;
    public void OnEnable()
    {
        updateIndex = Random.Range(0, updateRate);

    }


    // Update is called once per frame
    void Update()
    {

        if (Time.frameCount % updateRate != updateIndex)
        {
            return;
        }
        Vector3 position = transform.position;
        Vector3 offset = Vector3.one * radius;
        List<Vector2Int> hitIndex = CollisionSystem.GetIndexList(position - offset, position + offset);

        if (!CollisionSystem.CheckIfCollisonsAreInGrid(hitIndex))
        {
            gameObject.SetActive(false);
            return;
        }

        List<HurtBox> allObjectsNearby = CollisionSystem.GetAllObjectsFromList(hitIndex);

        foreach (HurtBox hurtBox in allObjectsNearby)
        {
            Vector3 hurtBoxPosition = hurtBox.transform.position;
            float hurtBoxRadius = hurtBox.Radius;

            if (Vector3.Distance(position, hurtBoxPosition) < radius + hurtBoxRadius)
            {
                hurtBox.gameObject.SetActive(false);
                gameObject.SetActive(false);
                return;
            }

        }

        pastHurtBoxes = allObjectsNearby;
    }


    // gizmo
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

