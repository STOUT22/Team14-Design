using System.Collections;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string pickUpKey = "e"; // Key to pick up the item
    public float pickupRange = 3f; // Maximum range to pick up the item
    public LayerMask ItemLayer; // Layer for the pickup items
    private PlayerAudio playerAudio;

    private void Update()
    {
        // Check if the player is pressing the pickUpKey and if there are items in range
        if (Input.GetKeyDown(pickUpKey))
        {
            // Use OverlapCircle to detect items in range of the player
            Collider2D item = Physics2D.OverlapCircle(transform.position, pickupRange, ItemLayer);
            

            if (item != null)
            {
                // Call the method to pick up the item
                PickUpItem(item.gameObject);
                playerAudio.PlayPickItemSound();
            }
        }
    }

    private void PickUpItem(GameObject item)
    {
        // Call any logic for item usage here (e.g., give the item to the player)
        Debug.Log(item.name + " picked up the item!");

        // Destroy the item after pickup
        Destroy(item);
    }

    // Optional: Draw the pickup range as a circle in the editor for debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
