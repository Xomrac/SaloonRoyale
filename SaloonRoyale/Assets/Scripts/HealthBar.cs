using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        var life = health.GetCurrentLife();
        UpdateHealth(life);
        health.OnHealthChanged += UpdateHealth;
    }

    private void UpdateHealth(int life)
    {
        healthText.text = life.ToString();
    }
}