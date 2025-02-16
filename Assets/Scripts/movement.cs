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

    private static int currentSceneIndex = -1; 

    void Start()
    {
        if (currentSceneIndex == -1) 
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        }

        targetSpeed = speed;
        rb = GetComponent<Rigidbody>(); 

        if (rb == null) 
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false; 
            rb.isKinematic = true; 
        }
    }

    void Update()
    {
        if (!isColliding) 
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A)) 
        {
            transform.Translate(Vector3.left * sideSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D)) 
        {
            transform.Translate(Vector3.right * sideSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.S)) 
        {
            targetSpeed = speed * slowDownFactor; 
        }
        else
        {
            targetSpeed = maxSpeed; 
        }

        speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime * 5f);

        if (speed > maxSpeed) 
        {
            speed = maxSpeed;
        }

        if (speed < minSpeed) 
        {
            speed = minSpeed;
        }

        if (isColliding)
        {
            rb.linearVelocity = Vector3.zero; 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle")) 
        {
            // Stop movement
            isColliding = true;
            speed = 0f; 
            rb.linearVelocity = Vector3.zero; 

           
            SceneManager.LoadScene(2); 
        } else if (other.CompareTag("FinishLine"))
        {
            SceneManager.LoadScene("LavaBane"); 
        } else if (other.CompareTag("Victory"))
        {
            SceneManager.LoadScene("Victory");
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game from scene index: " + currentSceneIndex); 
        if (currentSceneIndex == 0 || currentSceneIndex == 1)
        {
            SceneManager.LoadScene(currentSceneIndex); 
        }
        else
        {
            Debug.LogError("Invalid scene index for restarting game: " + currentSceneIndex);
        }
    }
}
