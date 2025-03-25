using UnityEngine;

public class HitBoxRayCast : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] float radius = 0.5f;



    // update 
    public void Update()
    {
        // only do this every 3 frame
        if (Time.frameCount % 3 != 0)
        {
            return;
        }

        // spere cast
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0, Vector3.forward, out hit, radius, layerMask))
        {
            
        }

        if (hit.collider != null)
        {
            // disabel other and self
            hit.collider.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }


    }

}
