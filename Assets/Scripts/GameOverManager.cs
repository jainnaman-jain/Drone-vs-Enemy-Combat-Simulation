
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Mission Failed UI")]
    [SerializeField] private GameObject missionFailedPanel;

    private bool isGameOver;

    private void Awake()
    {
        Time.timeScale = 1f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (missionFailedPanel != null)
            missionFailedPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (missionFailedPanel != null)
            missionFailedPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
