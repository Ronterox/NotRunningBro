
using UnityEngine;
using UnityEngine.UI;

public class HealthBar2D : MonoBehaviour
{
    public Slider healthBarSlider;

    private void Start()
    {
        if (healthBarSlider == null)
            Debug.Log("No healthBarSlider Detected");
    }
    public void SetMaxHealth(int maxHealth)
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;
    }

    public void SetHealth(int health)
    {
        healthBarSlider.value = health;
    }
}
