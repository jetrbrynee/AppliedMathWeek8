using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // The cube-shaped obstacle prefab.
    public float spawnInterval = 2.0f; // Time between obstacle spawns.
    public float moveSpeed = 5.0f; // Obstacle movement speed.
    public float spawnDistance = 10.0f; // Distance in front of the player to spawn obstacles.

    private float nextSpawnTime;
    private Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform; // Replace "Player" with the actual name of your player GameObject.
        nextSpawnTime = Time.time + spawnInterval; // Set the first spawn time.
    }

    void Update()
    {
        // Check if it's time to spawn a new obstacle based on the spawn interval.
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + spawnInterval; // Set the next spawn time.
        }

        // Move spawned obstacles towards the player.
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle"); // Assuming you tagged your obstacles as "Obstacle".

        foreach (GameObject obstacle in obstacles)
        {
            // Calculate the direction towards the player.
            Vector3 moveDirection = (player.position - obstacle.transform.position).normalized;

            // Move the obstacle towards the player.
            obstacle.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            // Remove obstacles that are too far behind the player.
            if (obstacle.transform.position.z < player.position.z - spawnDistance)
            {
                Destroy(obstacle);
            }
        }
    }

    void SpawnObstacle()
    {
        // Randomly adjust the obstacle's X position within the available lanes.
        float laneWidth = 1.0f; // Adjust based on your lane setup.
        float randomX = Random.Range(-laneWidth, laneWidth);

        // Calculate the position to spawn the obstacle in front of the player.
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, player.position.z + spawnDistance);

        // Create a new obstacle and set its position.
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        newObstacle.tag = "Obstacle"; // Tag the obstacle for movement.
    }
}
