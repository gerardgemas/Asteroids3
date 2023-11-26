using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Array of asteroid prefabs with different sizes
    public float spawnDistance = 10f; // Distance from the screen edge to spawn asteroids
    public float spawnInterval = 1f; // Interval between asteroid spawns
    private float spawnTimer = 0f; // Timer to track spawning
    public GameManager gameManager;
    void Update()
    {
        if (gameManager.getPlay()){
            // Increment the timer
            spawnTimer += Time.deltaTime;

            // Check if it's time to spawn an asteroid
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f; // Reset the timer

                // Call the function to spawn asteroids
                SpawnAsteroid();
            }
        }
    }

    void SpawnAsteroid()
    {
        Vector3 spawnPosition = CalculateSpawnPosition();

        // Randomly select an asteroid prefab from the array
        GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Instantiate the selected asteroid prefab at the spawn position
        Instantiate(asteroidPrefab, new Vector3(spawnPosition.x, spawnPosition.y, 0f), Quaternion.identity);
    }

    Vector3 CalculateSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;

        // Determine a random direction from the screen center
        Vector2 direction = Random.insideUnitCircle.normalized;

        // Calculate the spawn position outside the screen based on direction
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Spawn on the left or right side
            spawnPosition.x = direction.x > 0 ? screenWidth + spawnDistance : -spawnDistance;
            spawnPosition.y = Random.Range(0f, screenHeight);
        }
        else
        {
            // Spawn on the top or bottom side
            spawnPosition.x = Random.Range(0f, screenWidth);
            spawnPosition.y = direction.y > 0 ? screenHeight + spawnDistance : -spawnDistance;
        }

        return Camera.main.ScreenToWorldPoint(spawnPosition);
    }
}