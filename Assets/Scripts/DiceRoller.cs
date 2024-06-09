using UnityEngine;

public static class DiceRoller
{
    public static int RollDice(int sides, int rolls = 1)
    {
        int total = 0;
        Debug.Log($"{rolls} dice were rolled.");
        for (int i = 0; i < rolls; i++)
        {
            int roll = Random.Range(1, sides + 1);
            total += roll;
            Debug.Log($"Dice {i + 1}: rolled {roll}");
        }
        return total;
    }
}