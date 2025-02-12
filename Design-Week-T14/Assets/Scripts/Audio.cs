using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource movementSource;
    public AudioSource actionSource;

    [Header("Audio Clips")]
    public AudioClip moveClip;
    public AudioClip jumpClip;
    public AudioClip attackClip;
    public AudioClip pickItemClip;

    private Player player;
    private bool isMoving;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        HandleMovementSound();
    }

    void HandleMovementSound()
    {
        if (player.rb.velocity.x != 0 && !isMoving)
        {
            movementSource.clip = moveClip;
            movementSource.loop = true;
            movementSource.Play();
            isMoving = true;
        }
        else if (player.rb.velocity.x == 0 && isMoving)
        {
            movementSource.Stop();
            isMoving = false;
        }
    }

    public void PlayJumpSound()
    {
        actionSource.PlayOneShot(jumpClip);
    }

    public void PlayAttackSound()
    {
        actionSource.PlayOneShot(attackClip);
    }

    public void PlayPickItemSound()
    {
        actionSource.PlayOneShot(pickItemClip);
    }
}
