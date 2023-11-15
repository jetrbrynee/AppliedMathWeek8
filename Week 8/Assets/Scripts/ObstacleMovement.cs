using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5.0f; // Speed at which the obstacle moves towards the player
    private Vector3 movementDirection = Vector3.back; // Assuming the player is facing positive Z

    // Update is called once per frame
    void Update()
    {
        // Move the obstacle towards the player
        transform.position += movementDirection * speed * Time.deltaTime;

        // Check if the obstacle has passed the player
        if (transform.position.z < -10) // Assuming the player's Z position is at 0
        {
            DestroyObstacle();
        }
    }

    void DestroyObstacle()
    {
        // Destroy this game object
        Destroy(gameObject);
    }
}
