
using UnityEngine;
using TMPro;
using System;

public class DroneHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    [SerializeField] private TMP_Text healthText;

    [Header("Game Over")]
    [SerializeField] private GameOverManager gameOverManager;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0f)
            return;

        currentHealth = Mathf.Max(0f, currentHealth - damage);
        UpdateHealthUI();

        Debug.Log(
            $"Drone took {damage} damage. Health: {currentHealth}/{maxHealth}"
        );

        if (currentHealth <= 0f)
        {
            Debug.Log("Game Over! Drone destroyed.");

            if (gameOverManager != null)
                gameOverManager.ShowGameOver();

            Destroy(gameObject);
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text =
                $"Drone Health: {currentHealth:0} / {maxHealth:0}";
        }
    }
}
