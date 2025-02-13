using Unity.VisualScripting;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetSpeed = speed; 
    }

    // Update is called once per frame
    void Update()
    {
        // Move left when 'A' is pressed
        if (Input.GetKey(KeyCode.A)) 
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
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

        // Move forward with adjusted speed
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
    }
    }
    
    