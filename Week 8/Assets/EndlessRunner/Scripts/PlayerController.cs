using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 originalScale;
    private bool isJumping = false;
    private bool isDucking = false;

    public float jumpHeight = 2.0f; // Adjust the jump height as needed
    public float jumpDuration = 0.5f; // Adjust the jump animation duration
    public float duckDuration = 1.0f; // Adjust the duck animation duration

    private float jumpStartTime;

    void Start()
    {
        // Store the original scale at the start of the game
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Check for player input to trigger jump and duck
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isDucking)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.C) && !isJumping && !isDucking)
        {
            Duck();
        }

        // Check for the jump duration and reset the scale
        if (isJumping && Time.time - jumpStartTime > jumpDuration)
        {
            ResetScale();
        }
    }

    void Jump()
    {
        // Simulate jumping by adjusting the Y-scale
        Vector3 jumpScale = new Vector3(1, 2, 1); // Adjust the Y-value for the desired jump height
        transform.localScale = jumpScale;

        // Record the start time of the jump
        jumpStartTime = Time.time;

        // Set the jumping state
        isJumping = true;
    }

    void Duck()
    {
        // Simulate ducking by adjusting the Y-scale
        Vector3 duckScale = new Vector3(1, 0.5f, 1); // Adjust the Y-value for the desired duck height
        transform.localScale = duckScale;

        // Set the ducking state
        isDucking = true;

        // Start a timer to reset the scale after the duck duration
        Invoke("ResetScale", duckDuration);
    }

    void ResetScale()
    {
        // Reset the scale to the original scale
        transform.localScale = originalScale;

        // Reset the jumping and ducking states
        isJumping = false;
        isDucking = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Handle game over logic here.
            // For now, let's just print a message to the console.
            Debug.Log("Collision with an obstacle!");
        }
    }
}
