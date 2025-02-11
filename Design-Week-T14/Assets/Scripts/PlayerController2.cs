using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    private PlayerControls2 controls;
    private Rigidbody2D rb;

    public float moveSpeed = 5f;
    public float moveForce = 50f; // More force for movement
    public float jumpForce = 10f;
    public float maxSpeed = 5f;
    private bool isGrounded;

    private float moveInput = 0f;

    private void Awake()
    {
        controls = new PlayerControls2();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls.Player2.Enable();

        // D-Pad Left
        controls.Player2.MoveLeft.performed += ctx => moveInput = -1f;
        controls.Player2.MoveLeft.canceled += ctx => ResetMoveInput();

        // D-Pad Right
        controls.Player2.MoveRight.performed += ctx => moveInput = 1f;
        controls.Player2.MoveRight.canceled += ctx => ResetMoveInput();

        // Jump
        controls.Player2.Jump.performed += ctx => Jump();

        // Attack
        controls.Player2.Attack.performed += ctx => Attack();

        // Pick Item
        controls.Player2.PickItem.performed += ctx => PickUp();
    }

    private void OnDisable()
    {
        controls.Player2.Disable();
    }

    private void FixedUpdate()
    {
        Debug.Log($"Move Input: {moveInput} | Rigidbody Velocity: {rb.velocity}");

        MovePlayer();
    }

    private void MovePlayer()
    {
        if (moveInput != 0)
        {
            // Apply force for movement instead of setting velocity directly
            rb.AddForce(new Vector2(moveInput * moveForce, 0), ForceMode2D.Force);

            // Cap speed to prevent excessive acceleration
            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            }
        }
    }

    private void ResetMoveInput()
    {
        moveInput = 0f;
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void Attack()
    {
        Debug.Log("Attack with B!");
    }

    private void PickUp()
    {
        Debug.Log("Pick up item with Y!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
