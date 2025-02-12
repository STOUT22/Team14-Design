using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float gravityScale = 2f;

    public Rigidbody2D rb { get; private set; }
    private Vector2 movement;
    private bool isGrounded;
    private bool hasWeapon;
    private GameObject weaponObject;
    private ItemSpawner itemSpawner;
    private PlayerAudio playerAudio;

    private int currentHealth;
    public int maxHealth = 100;

    private bool isDead = false;
    private GameController gameController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        hasWeapon = false;
        weaponObject = null;
        itemSpawner = FindObjectOfType<ItemSpawner>();
        playerAudio = GetComponent<PlayerAudio>();
        gameController = FindObjectOfType<GameController>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;  // Skip logic if player is dead

        HandleInput();
    }

    void FixedUpdate()
    {
        if (isDead) return;  // Skip movement if player is dead

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
        playerAudio.PlayJumpSound();
    }

    void OnCollisionEnter2D(Collision2D collision)
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

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            weaponObject = null;
        }
    }

    void Attack()
    {
        if (hasWeapon)
        {
            Debug.Log(gameObject.tag + " attacked with weapon!");
            playerAudio.PlayAttackSound();
            UseItem();
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
            hasWeapon = true;
            Debug.Log(gameObject.tag + " picked up a weapon!");
            playerAudio.PlayPickItemSound();
            UseItem();
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
            itemSpawner.ItemUsed(weaponObject);
            weaponObject = null;
            hasWeapon = false;
        }
    }

    // Health management
    public void TakeDamage(int damage)
    {
        if (isDead) return;  // Ignore damage if already dead

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;  // Stop all movement
        gameController.UpdatePlayerHealth(gameObject.tag, 0);  // Update health bar to 0

        Debug.Log(gameObject.tag + " has died!");

        // Trigger respawn after a delay
        Invoke("Respawn", 3f);  // Respawn after 3 seconds (you can adjust this)

        // Notify GameController that a player died (so the other player gets +1 point)
        gameController.OnPlayerDeath(gameObject.tag);
    }


    void Respawn()
    {
        // Respawn the player at their starting position
        transform.position = Vector3.zero;  // Respawn at the origin or wherever you want
        currentHealth = maxHealth;
        gameController.UpdatePlayerHealth(gameObject.tag, currentHealth);  // Reset health bar

        isDead = false;  // Allow player to move again
        Debug.Log(gameObject.tag + " has respawned!");
    }
}
