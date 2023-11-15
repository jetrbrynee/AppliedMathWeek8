using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float laneDistance = 2f; // The distance between the lanes
    public float jumpHeight = 2f; // How high the jump is
    public float jumpSpeed = 4f; // How fast the jump happens
    public float duckHeight = 0.5f; // How low the duck is
    public float duckSpeed = 4f; // How fast the duck happens

    private int currentLane = 1; // Start in the middle lane
    private Vector3 verticalTargetPosition;
    private bool isJumping = false;
    private bool isDucking = false;

    private void Update()
    {
        // Lane movement
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
        {
            currentLane--;
            MoveToLane(currentLane);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2)
        {
            currentLane++;
            MoveToLane(currentLane);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping && !isDucking)
        {
            StartCoroutine(Jump());
        }

        // Ducking
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isDucking && !isJumping)
        {
            StartCoroutine(Duck());
        }
    }

    void MoveToLane(int lane)
    {
        Vector3 newPosition = transform.position;
        newPosition.x = (lane - 1) * laneDistance;
        transform.position = newPosition;
    }

    System.Collections.IEnumerator Jump()
    {
        isJumping = true;
        float startY = transform.position.y;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * jumpSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            float newY = startY + jumpHeight * interpolation;
            verticalTargetPosition.y = newY;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        verticalTargetPosition.y = startY;
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        isJumping = false;
    }

    System.Collections.IEnumerator Duck()
    {
        isDucking = true;
        float startY = transform.position.y;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * duckSpeed;
            float newY = Mathf.Lerp(startY, duckHeight, percent);
            verticalTargetPosition.y = newY;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        // Assumes the player returns to standing up instantly after ducking
        verticalTargetPosition.y = startY;
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        isDucking = false;
    }
}