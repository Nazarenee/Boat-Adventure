using UnityEngine;
using UnityEngine.SceneManagement; 

public class victoryscript : MonoBehaviour
{

    
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
        
  
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}