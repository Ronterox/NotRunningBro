
using UnityEngine;

public class HealthManager2D : MonoBehaviour
{
    public HealthBar2D healthBar = null;

    public int maxHealth = 100;

    public int currentHealth;

    private void Start()
    {
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        Debug.Log("Character took " + damage + " damage.\nCurrent HP: " + currentHealth);
    }
}
