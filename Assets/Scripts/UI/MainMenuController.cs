using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject controlsPanel;

    public void ShowControls()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        if (controlsPanel != null)
            controlsPanel.SetActive(true);
    }

    public void ShowMainMenu()
    {
        if (controlsPanel != null)
            controlsPanel.SetActive(false);

        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }

    public void StartMission()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
