using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassThroughPlatform : MonoBehaviour
{
    private Collider2D collider;
    private bool playerOnPlatform;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D collision, bool value)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            playerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetPlayerOnPlatform(collision, true);
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        SetPlayerOnPlatform(collision, true);
    }
}
