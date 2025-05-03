using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameControls : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // For Editor
#else
        Application.Quit(); // For builds
#endif
    }
}
