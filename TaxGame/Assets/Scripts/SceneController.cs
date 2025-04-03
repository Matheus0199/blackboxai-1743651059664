using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject gameManagerPrefab;
    public GameObject uiControllerPrefab;
    public GameObject eventSystemPrefab;

    void Awake()
    {
        // Ensure essential game objects exist
        if (GameManager.Instance == null)
        {
            Instantiate(gameManagerPrefab);
        }

        if (FindObjectOfType<UIController>() == null)
        {
            Instantiate(uiControllerPrefab);
        }

        if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            Instantiate(eventSystemPrefab);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ShowTutorial()
    {
        // Implement tutorial logic here
        Debug.Log("Showing tutorial...");
    }
}