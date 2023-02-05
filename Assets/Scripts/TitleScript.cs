using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

//execute this after game manager to make sure its all loaded up first before setting up the ui
[DefaultExecutionOrder(1000)]

/// <summary>
/// Title menu options functionality.
/// </summary>
public class TitleScript : MonoBehaviour
{
    /// <summary>
    /// Begin the game.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("MainLevel");
    }

    /// <summary>
    /// Exit and close the game.
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
