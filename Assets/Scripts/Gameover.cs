using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) // Fixed parentheses here
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(1); // Added semicolon here
        }
    }
}