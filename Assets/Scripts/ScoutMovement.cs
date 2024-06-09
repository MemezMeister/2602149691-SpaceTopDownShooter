using UnityEngine;
using System.Collections;

public class ScoutMovement : MonoBehaviour
{
    public float detectDistance = 10f;
    public float strafingSpeed = 3f;
    public float chasingSpeed = 5f;
    private Transform playerTransform;
    private bool isStrafing = false;
    private float strafeDirection = -2.5f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            if (playerTransform == null) return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer < detectDistance)
        {
            if (!isStrafing)
            {
                isStrafing = true;
                StartCoroutine(Strafe());
            }
        }
        else
        {
            isStrafing = false;
            StopAllCoroutines();
            ChasePlayer();
        }
        RotateTowardsPlayer();
    }

    IEnumerator Strafe()
    {
        while (isStrafing)
        {
            Vector3 targetPosition = playerTransform.position + (Vector3.right * strafeDirection);
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, strafingSpeed * Time.deltaTime);
                yield return null;
            }
            strafeDirection *= -1;
            yield return null;
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, chasingSpeed * Time.deltaTime);
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
