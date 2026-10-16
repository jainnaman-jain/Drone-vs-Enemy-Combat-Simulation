
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("Health Bar")]
    [SerializeField] private Image healthBarFill;
    [SerializeField] private GameObject healthBarCanvas;

    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0f)
            return;

        currentHealth = Mathf.Max(0f, currentHealth - damage);

        UpdateHealthBar();

        Debug.Log(
            $"Enemy {name} took {damage} damage. " +
            $"Health: {currentHealth}/{maxHealth}"
        );

        if (currentHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount =
                maxHealth > 0f ? currentHealth / maxHealth : 0f;
        }

        if (healthBarCanvas != null)
        {
            healthBarCanvas.SetActive(currentHealth > 0f);
        }
    }
}
