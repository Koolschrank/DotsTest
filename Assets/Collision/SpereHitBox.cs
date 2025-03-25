using UnityEngine;

public class SpereHitBox : MonoBehaviour
{
    // on trigger enter
    /*private void OnTriggerEnter(Collider other)
    {

        // disable self and other
        gameObject.SetActive(false);
        other.gameObject.SetActive(false);
        Debug.Log("Collision");
    }*/

    // on collision stay
    // on trigger stay
    private void OnTriggerStay(Collider other)
    {
        // disable self and other
        gameObject.SetActive(false);
        other.gameObject.SetActive(false);
        Debug.Log("Collision");
    }
}
