using UnityEngine;

public class Movement : MonoBehaviour, IMovement
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    public void SetDirection(float horizontal, float vertical)
    {
        movementDirection = new Vector2(horizontal, vertical).normalized;
    }

    void Move()
    {
        rb.velocity = movementDirection * speed;
    }
}