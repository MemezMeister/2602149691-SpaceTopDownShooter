using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Player Data", order = 51)]
public class PlayerData : ScriptableObject
{
    public int maxHealth;
    public int damage;
    public GameObject prefab;
}