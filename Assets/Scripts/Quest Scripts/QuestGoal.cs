using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public string objectName;

    public int requiredAmount;
    public int currentAmount;

/*    public bool isReached()
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
    }*/

    public bool isReached()
    {
        return currentAmount >= requiredAmount;
    }

    public void EnemyDefeated()
    {
        if(goalType == GoalType.Defeat)
        {
            currentAmount++;
        }
    }
    public void ItemCollected()
    {
        if(goalType == GoalType.Gathering)
        {
            currentAmount++;
        }
    }

}

public enum GoalType
{
    Defeat,
    Gathering,
    Talk
}
