using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float gravityScale = 2f; // Gravity multiplier

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isGrounded;
    private bool hasWeapon; // Track if the player has a weapon
    private GameObject weaponObject; // Reference to the weapon object when in range
    private ItemSpawner itemSpawner; // Reference to ItemSpawner to notify item usage

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale; // Apply gravity scale
        hasWeapon = false; // Initially, the player doesn't have the weapon
        weaponObject = null; // No weapon in range initially
        itemSpawner = FindObjectOfType<ItemSpawner>(); // Find the ItemSpawner in the scene
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
            if (Input.GetKeyDown(KeyCode.Q)) PickItem(); // Pick up item when Q is pressed
        }
        else if (CompareTag("Player2"))
        {
            if (Input.GetKey(KeyCode.RightArrow)) movement.x = 1;
            if (Input.GetKey(KeyCode.LeftArrow)) movement.x = -1;
            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded) Jump();
            if (Input.GetKeyDown(KeyCode.RightShift)) Attack();
            if (Input.GetKeyDown(KeyCode.RightControl)) PickItem(); // Pick up item when Right Ctrl is pressed
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
        else if (collision.gameObject.CompareTag("Weapon"))
        {
            // Set the weapon object when in range
            weaponObject = collision.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            // Clear the weapon object reference when leaving the collision zone
            weaponObject = null;
        }
    }

    void Attack()
    {
        if (hasWeapon)
        {
            Debug.Log(gameObject.tag + " attacked with weapon!");
            // Add attack logic here
            UseItem(); // Item gets used
        }
        else
        {
            Debug.Log(gameObject.tag + " tried to attack but has no weapon!");
        }
    }

    void PickItem()
    {
        if (weaponObject != null)
        {
            hasWeapon = true; // Mark the player as having picked up a weapon
            Debug.Log(gameObject.tag + " picked up a weapon!");
            UseItem(); // Item gets used
        }
        else
        {
            Debug.Log(gameObject.tag + " tried to pick up a weapon but there's no weapon nearby!");
        }
    }

    void UseItem()
    {
        if (weaponObject != null)
        {
            // Notify the ItemSpawner that the item has been used and destroy it
            itemSpawner.ItemUsed(weaponObject);
            weaponObject = null; // Clear the reference after use
            hasWeapon = false; // Reset weapon status for the player
        }
    }
}
