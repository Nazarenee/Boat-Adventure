using UnityEngine;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        // Optional: You can add some effects like animation or visuals to show the boat is stopped
        if (isColliding)
        {
            rb.linearVelocity = Vector3.zero; // Ensure the boat is fully stopped (no movement).
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // Stop the boat when colliding with an obstacle
            isColliding = true;
            speed = 0f; // Immediately stop the boat
            rb.linearVelocity = Vector3.zero; // Ensure the boat stops moving
            print("Boat has collided with obstacle, speed set to 0.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // When the boat exits the obstacle's collider, resume movement
            isColliding = false;
            print("Boat is no longer colliding with obstacle, speed will resume.");
        }
    }
}
