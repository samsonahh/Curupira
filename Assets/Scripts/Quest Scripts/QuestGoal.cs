using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public string[] objectNames;

    public int[] requiredAmount;
    public int[] currentAmount;

    public bool isReached()
    {
        for (int i = 0; i < currentAmount.Length; i++)
        {
            if(currentAmount[i] >= requiredAmount[i])
            {
                continue;
            }
            return false;
        }
        return true;
    }

    public void EnemyDefeated()
    {
        if(goalType == GoalType.Defeat)
        {
            currentAmount[0]++;
        }
    }
    public void ItemCollected()
    {
        if(goalType == GoalType.Gathering)
        {
            currentAmount[0]++;
        }
    }

}

public enum GoalType
{
    Defeat,
    Gathering,
    Talk
}
