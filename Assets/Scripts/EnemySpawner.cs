using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public Transform playerTransform;
    public float spawnDistance = 9.0f;
    public float spawnRangeX = 10.0f;
    public List<GameObject> ActiveEnemies { get; private set; } = new List<GameObject>();
    private int initialRolls = 1;

    private HashSet<Vector2> usedSpawnPositions = new HashSet<Vector2>();

    public void SpawnEnemiesForRound(int round, EnemyData spaceFighterLeaderData, EnemyData spaceFighterData, EnemyData scoutData)
    {
        int rolls = (round > 1) ? (round + 1) / 2 : initialRolls;

        int i = 0;
        if (round == 1)
        {
            i = 0; 
        }

        for (; i < rolls; i++)
        {
            int diceRoll = DiceRoller.RollDice(20);
            if (diceRoll >= 1 && diceRoll <= 5)
            {
                SpawnEnemy(spaceFighterLeaderData);
                SpawnEnemies(spaceFighterData, 1);
            }
            else if (diceRoll >= 6 && diceRoll <= 10)
            {
                SpawnEnemies(spaceFighterData, 2);
            }
            else if (diceRoll >= 11 && diceRoll <= 15)
            {
                SpawnEnemies(scoutData, 2);
            }
            else if (diceRoll >= 16 && diceRoll <= 20)
            {
                SpawnEnemy(spaceFighterData);
            }
        }
    }

    public void SpawnEnemy(EnemyData enemyData)
    {
        Vector2 spawnPosition = GetUniqueSpawnPosition();
        GameObject enemy = Instantiate(enemyData.prefab, spawnPosition, Quaternion.identity);
        enemy.tag = "Enemy";

        EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
        if (enemyBase != null)
        {
            enemyBase.currentHealth = enemyData.baseHealth;
            enemyBase.enemyData = enemyData;
        }

        ActiveEnemies.Add(enemy);
        AdjustSpawnPosition(enemy);
    }

    public void SpawnEnemies(EnemyData enemyData, int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy(enemyData);
        }
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        ActiveEnemies.Remove(enemy);
        GameManager.Instance.OnEnemyDestroyed();
    }

    private Vector2 GetUniqueSpawnPosition()
    {
        Vector2 spawnPosition;
        int attempts = 0;
        do
        {
            float x = Random.Range(-spawnRangeX, spawnRangeX);
            spawnPosition = (Vector2)playerTransform.position + Vector2.up * spawnDistance + new Vector2(x, 0);
            attempts++;
        } while (usedSpawnPositions.Contains(spawnPosition) && attempts < 100);

        usedSpawnPositions.Add(spawnPosition);
        return spawnPosition;
    }

    private void AdjustSpawnPosition(GameObject enemy)
    {
        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
        if (enemyCollider != null)
        {
            usedSpawnPositions.Add((Vector2)enemy.transform.position + (Vector2)enemyCollider.bounds.extents);
        }
    }

    public void ResetDiceRollCount()
    {
        initialRolls = 1;
    }
}
