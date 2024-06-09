using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float stoppingDistance = 1.5f;
    private Transform playerTransform;

    private Vector2 lastPosition;
    private float timeSinceLastMove;
    private float checkInterval = 3.0f; 
    private bool isMoving = false; 

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        lastPosition = transform.position;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (distance > stoppingDistance)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }

        CheckMovement();
    }

    private void CheckMovement()
    {
        if ((Vector2)transform.position == lastPosition)
        {
            timeSinceLastMove += Time.deltaTime;
        }
        else
        {
            timeSinceLastMove = 0;
            lastPosition = transform.position;
        }

        if (timeSinceLastMove >= checkInterval && !isMoving)
        {
            ForceMoveOrDestroy();
        }
    }

    private void ForceMoveOrDestroy()
    {
        if (playerTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        if (Vector2.Distance(transform.position, playerTransform.position) > stoppingDistance)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
        timeSinceLastMove = 0; 
    }
}
