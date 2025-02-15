using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public float speed = 1f;
    public float sideSpeed = 6f;
    public float maxSpeed = 10f;
    public float acceleration = 0.1f;
    public float slowDownFactor = 0.3f;
    public float minSpeed = 2f;
    private float targetSpeed;
    private bool isColliding = false;

    private Rigidbody rb;

    private static int currentSceneIndex = -1; // Variable to hold the current scene index

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (currentSceneIndex == -1) // Only set it once
        {
            // Only store the level index, not the GameOver index
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Log for debugging purposes
            Debug.Log("Initial scene index: " + currentSceneIndex);
        }

        targetSpeed = speed;
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the boat

        if (rb == null) 
        {
            rb = gameObject.AddComponent<Rigidbody>(); // Add Rigidbody if it's missing
            rb.useGravity = false; // Disable gravity
            rb.isKinematic = true; // Make it kinematic
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isColliding) // Only move forward when not colliding with the obstacle
        {
            // Move forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }

        // Move left when 'A' is pressed
        if (Input.GetKey(KeyCode.A)) 
        {
            transform.Translate(Vector3.left * sideSpeed * Time.deltaTime, Space.World);
        }

        // Move right when 'D' is pressed
        if (Input.GetKey(KeyCode.D)) 
        {
            transform.Translate(Vector3.right * sideSpeed * Time.deltaTime, Space.World);
        }

        // Slow down when 'S' is held, but don't go below minSpeed
        if (Input.GetKey(KeyCode.S)) 
        {
            targetSpeed = speed * slowDownFactor; // Set target speed to the slowed-down value
        }
        else
        {
            targetSpeed = maxSpeed; // Revert to max speed when S is released
        }

        // Smoothly transition to target speed
        speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime * 5f);

        // Make sure speed doesn't exceed maxSpeed or go below minSpeed
        if (speed > maxSpeed) 
        {
            speed = maxSpeed;
        }

        if (speed < minSpeed) 
        {
            speed = minSpeed;
        }

        // Ensure the boat doesn't move after collision
        if (isColliding)
        {
            rb.linearVelocity = Vector3.zero; // Stop the boat completely
        }
    }

    // This method will handle the collision with obstacles
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle")) // Ensure the object hit has the correct tag
        {
            // Stop movement
            isColliding = true;
            speed = 0f; // Stop the boat immediately
            rb.linearVelocity = Vector3.zero; // Stop the Rigidbody's movement

            // Trigger Game Over screen
            SceneManager.LoadScene(2); // Load GameOver scene (index 2)
        }
    }

    // Method to restart the game
    public void RestartGame()
    {
        Debug.Log("Restarting game from scene index: " + currentSceneIndex); // Debugging to check the stored scene index
        // Only restart the game if the scene index is valid (0 or 1)
        if (currentSceneIndex == 0 || currentSceneIndex == 1)
        {
            SceneManager.LoadScene(currentSceneIndex); // Load the level scene
        }
        else
        {
            Debug.LogError("Invalid scene index for restarting game: " + currentSceneIndex);
        }
    }
}
