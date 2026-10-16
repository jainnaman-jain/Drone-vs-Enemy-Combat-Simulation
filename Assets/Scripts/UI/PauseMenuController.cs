
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("Pause UI")]
    [SerializeField] private GameObject pausePanel;

    private bool isPaused;

    private void Start()
    {
        SetPaused(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        SetPaused(true);
    }

    public void ResumeGame()
    {
        SetPaused(false);
    }

    public void RestartMission()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void SetPaused(bool paused)
    {
        isPaused = paused;
        Time.timeScale = isPaused ? 0f : 1f;

        if (pausePanel != null)
            pausePanel.SetActive(isPaused);

        Cursor.visible = isPaused;
        Cursor.lockState = isPaused
            ? CursorLockMode.None
            : CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
