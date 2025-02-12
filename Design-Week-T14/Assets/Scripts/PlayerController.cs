using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;
    private Rigidbody2D rb;
    private PlayerAudio playerAudio; // Reference to PlayerAudio

    public float moveSpeed = 5f;
    public float moveForce = 50f; // More force for movement
    public float jumpForce = 10f;
    public float maxSpeed = 5f;
    private bool isGrounded;

    private float moveInput = 0f;

    private bool hasWeapon = false; // Track if the player has the weapon
    private GameObject weaponObject = null; // Reference to the weapon object when in range

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<PlayerAudio>(); // Get reference to PlayerAudio component
    }

    private void OnEnable()
    {
        controls.Player1.Enable();

        // D-Pad Left
        controls.Player1.MoveLeft.performed += ctx => { moveInput = -1f; playerAudio.movementSource.Play(); };
        controls.Player1.MoveLeft.canceled += ctx => ResetMoveInput();

        // D-Pad Right
        controls.Player1.MoveRight.performed += ctx => { moveInput = 1f; playerAudio.movementSource.Play(); };
        controls.Player1.MoveRight.canceled += ctx => ResetMoveInput();

        // Jump
        controls.Player1.Jump.performed += ctx => Jump();

        // Attack
        controls.Player1.Attack.performed += ctx => Attack();

        // Pick Item
        controls.Player1.PickItem.performed += ctx => PickUp();
    }

    private void OnDisable()
    {
        controls.Player1.Disable();
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
            rb.AddForce(new Vector2(moveInput * moveForce, 0), ForceMode2D.Force);
            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            }
        }
    }

    private void ResetMoveInput()
    {
        moveInput = 0f;
        playerAudio.movementSource.Stop();
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            playerAudio.PlayJumpSound();
        }
    }

    private void Attack()
    {
        if (hasWeapon)
        {
            Debug.Log("Attack with weapon!");
            playerAudio.PlayAttackSound();
        }
        else
        {
            Debug.Log("Tried to attack but no weapon equipped.");
        }
    }

    private void PickUp()
    {
        if (weaponObject != null)
        {
            hasWeapon = true;
            Debug.Log("Picked up a weapon!");
            playerAudio.PlayPickItemSound();
            Destroy(weaponObject);
            weaponObject = null;
        }
        else
        {
            Debug.Log("No weapon nearby to pick up.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Weapon"))
        {
            weaponObject = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            weaponObject = null;
        }
    }
}
