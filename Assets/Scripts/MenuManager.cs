using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Public variable to set the scene name in the Unity Inspector
    public string sceneToLoad;

    // Method to load the scene of your choice
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // Method to quit the game
    public void QuitGame()
    {
        // If we are in the editor, stop the play mode
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If we're in a built game, quit
            Application.Quit();
        #endif
    }
}
