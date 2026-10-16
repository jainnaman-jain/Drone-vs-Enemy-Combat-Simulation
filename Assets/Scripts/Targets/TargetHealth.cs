
using UnityEngine;

public class TargetHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 50f;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0f)
            return;

        currentHealth -= damage;

        Debug.Log(
            gameObject.name + " took " + damage +
            " damage. Remaining health: " +
            Mathf.Max(currentHealth, 0f)
        );

        if (currentHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
