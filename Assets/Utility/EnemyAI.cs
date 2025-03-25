using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        target = Player.Instance.transform;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    }


    // on disable
    private void OnDisable()
    {
        transform.position =Vector3.left * 1000;
    }
}
