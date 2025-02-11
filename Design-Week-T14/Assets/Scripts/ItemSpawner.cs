using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Array of item prefabs (e.g., weapons)
    public Transform[] spawnPoints; // Three dedicated spawn points
    private List<GameObject> spawnedItems = new List<GameObject>(); // List of spawned items

    public int maxItems = 3; // Max number of items to be spawned
    public float spawnInterval = 5f; // Time interval for spawning items (in seconds)

    private void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnItemsCoroutine());
    }

    private IEnumerator SpawnItemsCoroutine()
    {
        while (true)
        {
            // Check if there are fewer than maxItems in the scene
            if (spawnedItems.Count < maxItems)
            {
                SpawnItem();
            }

            // Wait for the specified spawn interval before checking again
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnItem()
    {
        // Find all spawn points and randomly select one to spawn an item
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnedItems.Count < maxItems)
            {
                // Randomly select an item prefab
                GameObject itemToSpawn = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

                // Instantiate the item at the spawn point
                GameObject spawnedItem = Instantiate(itemToSpawn, spawnPoints[i].position, Quaternion.identity);
                spawnedItems.Add(spawnedItem); // Add to the list of spawned items
                Debug.Log(itemToSpawn.name + " spawned at " + spawnPoints[i].position);
            }
        }
    }

    // This method is called when a player uses an item (e.g., weapon)
    public void ItemUsed(GameObject item)
    {
        // Destroy the item and remove it from the spawnedItems list
        if (spawnedItems.Contains(item))
        {
            spawnedItems.Remove(item);
            Destroy(item);
            Debug.Log(item.name + " has been used and destroyed.");
        }
    }

    private void OnDrawGizmos()
    {
        // Draw spawn points in the editor for easy visualization
        foreach (Transform spawnPoint in spawnPoints)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(spawnPoint.position, 0.5f);
        }
    }
}
