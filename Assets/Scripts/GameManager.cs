using UnityEngine;
using UnityEngine.SceneManagement; // for scene management

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        
        SceneManager.LoadScene("GameScene"); 
    }
    
     public void ExitGame()
        {
            Debug.Log("Exiting the game...");
    
            // 
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
}
