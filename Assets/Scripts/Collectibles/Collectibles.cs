using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField] private float spawnRate = 10f;
    [SerializeField] private float xPos = 2f;
    [SerializeField] private float yPos = 6f;

    private float timeSinceLastSpawned;
    private Vector3 currentSpawnPos;

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if(timeSinceLastSpawned>=spawnRate)
        {
            SpawnCollectibles();
            timeSinceLastSpawned = 0f;
        }

        
    }

    private void SpawnCollectibles()
    {
        Debug.Log("Collectible Spawned");
        float spawnXPos = Random.Range(-xPos, xPos);

        currentSpawnPos = new Vector2(spawnXPos, yPos);

        PoolManager.Instance.SpawnInWorld("Collectible", currentSpawnPos, Quaternion.identity);

        
    }
}
