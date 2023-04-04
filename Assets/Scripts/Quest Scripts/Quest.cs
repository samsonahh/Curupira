using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;

    public string title;
    public string description;
    public string[] sentences;

    public QuestGoal[] goals;

    public bool isQuestFullyComplete()
    {
        for(int i = 0; i < goals.Length; i++)
        {
            if (!goals[i].isReached())
            {
                return false;
            }
        }
        return true;
    }

    public void Complete()
    {
        isActive = false;
        Debug.Log(title + " was completed");
    }
}
