using UnityEngine;

public class Orb : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyBuff(other.GetComponent<PlayerHealth>());
            Destroy(gameObject);
        }
    }

    private void ApplyBuff(PlayerHealth player)
    {
        int diceRoll = Random.Range(1, 21);

        if (diceRoll >= 1 && diceRoll <= 5)
        {
            player.GetComponent<PlayerFiring>().fireRate -= 0.05f;
            DisplayBuffMessage("Fire rate increased!");
        }
        else if (diceRoll >= 6 && diceRoll <= 11)
        {
            player.Heal(10);
            DisplayBuffMessage("Health restored!");
        }
        else if (diceRoll >= 12 && diceRoll <= 16)
        {
            player.IncreaseMaxHealth(5);
            DisplayBuffMessage("Max health increased!");
        }
        else if (diceRoll >= 17 && diceRoll <= 20)
        {
            player.GetComponent<Bullet>().damage += 1;
            DisplayBuffMessage("Damage increased!");
        }
    }

    private void DisplayBuffMessage(string message)
    {
        GameManager.Instance.DisplayBuffMessage(message);
    }
}
