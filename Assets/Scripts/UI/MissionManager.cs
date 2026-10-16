
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    [Header("Mission UI")]
    [SerializeField] private GameObject missionCompletePanel;

    [Header("Mission Settings")]
    [SerializeField] private float checkInterval = 0.25f;

    private float nextCheckTime;
    private bool missionEnded;

    private void Start()
    {
        if (missionCompletePanel != null)
            missionCompletePanel.SetActive(false);
    }

    private void Update()
    {
        if (missionEnded || Time.time < nextCheckTime)
            return;

        nextCheckTime = Time.time + checkInterval;

        // Drone destruction means the mission was failed, not completed.
        if (FindFirstObjectByType<DroneHealth>() == null)
            return;

        EnemyHealth[] remainingEnemies =
            FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);

        if (remainingEnemies.Length == 0)
            CompleteMission();
    }

    private void CompleteMission()
    {
        missionEnded = true;

        if (missionCompletePanel != null)
            missionCompletePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void RetryMission()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
