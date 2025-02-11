using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float gravityScale = 2f; // Gravity multiplier

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale; // Apply gravity scale
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void HandleInput()
    {
        movement.x = 0;
        if (CompareTag("Player1"))
        {
            if (Input.GetKey(KeyCode.D)) movement.x = 1;
            if (Input.GetKey(KeyCode.A)) movement.x = -1;
            if (Input.GetKeyDown(KeyCode.W) && isGrounded) Jump();
            if (Input.GetKeyDown(KeyCode.E)) Attack();
            if (Input.GetKeyDown(KeyCode.Q)) PickItem();
        }
        else if (CompareTag("Player2"))
        {
            if (Input.GetKey(KeyCode.RightArrow)) movement.x = 1;
            if (Input.GetKey(KeyCode.LeftArrow)) movement.x = -1;
            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded) Jump();
            if (Input.GetKeyDown(KeyCode.RightShift)) Attack();
            if (Input.GetKeyDown(KeyCode.RightControl)) PickItem();
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void Attack()
    {
        Debug.Log(gameObject.tag + " attacked!");
        // Add attack logic here
    }

    void PickItem()
    {
        Debug.Log(gameObject.tag + " picked up an item!");
        // Add item pickup logic here
    }
}
