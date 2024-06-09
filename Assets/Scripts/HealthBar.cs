using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public bool showHealthBar = true;
    public EnemyBase enemyBase;

    void Start()
    {
        enemyBase = GetComponentInParent<EnemyBase>();
        if (!showHealthBar)
        {
            healthText.gameObject.SetActive(false);
        }
    }

    void LateUpdate()
    {
        if (showHealthBar && enemyBase != null)
        {
            healthText.text = $"{enemyBase.currentHealth}/{enemyBase.enemyData.baseHealth}";
            Vector3 healthBarPosition = Camera.main.WorldToScreenPoint(enemyBase.transform.position + Vector3.up * 0.5f);
            healthText.transform.position = healthBarPosition;
        }
    }
}
