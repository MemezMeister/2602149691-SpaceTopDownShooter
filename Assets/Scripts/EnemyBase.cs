using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public EnemyData enemyData;
    public int currentHealth;

    void Start()
    {
        currentHealth = enemyData.baseHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        DropOrbs();
        Destroy(gameObject);
        GameManager.Instance.OnEnemyDestroyed();
    }

    void DropOrbs()
    {
        int diceRoll = Random.Range(1, 21);
        if (diceRoll >= 17 && diceRoll <= 19)
        {
            Instantiate(GameManager.Instance.orbPrefab, transform.position, Quaternion.identity);
        }
        else if (diceRoll == 20)
        {
            Instantiate(GameManager.Instance.orbPrefab, transform.position, Quaternion.identity);
            Instantiate(GameManager.Instance.orbPrefab, transform.position, Quaternion.identity);
        }
    }
}

