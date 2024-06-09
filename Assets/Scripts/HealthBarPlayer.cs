using UnityEngine;
using TMPro;

public class HealthBarPlayer : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    void LateUpdate()
    {
        if (playerHealth != null && healthText != null)
        {
            healthText.text = $"{playerHealth.currentHealth}/{playerHealth.maxHealth}";
            Vector3 healthBarPosition = Camera.main.WorldToScreenPoint(playerHealth.transform.position + Vector3.up * -0.5f);
            healthText.transform.position = healthBarPosition;
        }
    }
}
