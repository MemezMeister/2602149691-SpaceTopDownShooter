using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy Data", order = 51)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int baseHealth;
    public int damage;
    public GameObject prefab;
}