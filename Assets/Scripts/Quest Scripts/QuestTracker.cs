using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestTracker : MonoBehaviour
{
    public GameObject questTracker;
    public TMP_Text titleText;
    public TMP_Text taskText;
    public TMP_Text progressText;

    public bool isQuestActive;

    // Start is called before the first frame update
    void Start()
    {
        questTracker = GameObject.Find("TrackerCanvas");
        titleText = questTracker.GetComponentsInChildren<TMP_Text>()[0];
        taskText = questTracker.GetComponentsInChildren<TMP_Text>()[1];
        progressText = questTracker.GetComponentsInChildren<TMP_Text>()[2];

        questTracker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        isQuestActive = MainManager.Instance.isQuestActive();

        if (isQuestActive)
        {
            questTracker.SetActive(true);
            titleText.text = MainManager.Instance.currentQuest.title;
            taskText.text = MainManager.Instance.currentQuest.description;

            HandleGoalTypes();
        }

        if (GameObject.Find("Player").GetComponent<PlayerManager>().isInteracting)
        {
            questTracker.SetActive(false);
        }
        else
        {
            questTracker.SetActive(true);
        }
    }

    public void HandleGoalTypes()
    {
        if (MainManager.Instance.currentQuest.isQuestFullyComplete())
        {
            progressText.color = Color.green;
        }
        else
        {
            progressText.color = Color.red;
        }

        if (MainManager.Instance.currentQuest.goals.Length == 1)
        {
            if(MainManager.Instance.currentQuest.goals[0].goalType == GoalType.Talk)
            {
                progressText.text = "";
                return;
            }
            if (MainManager.Instance.currentQuest.goals[0].goalType == GoalType.Defeat)
            {
                progressText.text = MainManager.Instance.currentQuest.goals[0].objectName;
                return;
            }
        }

        string progressString = "";

        for (int i = 0; i < MainManager.Instance.currentQuest.goals.Length; i++)
        {
            progressString += MainManager.Instance.currentQuest.goals[i].objectName + ": " + MainManager.Instance.currentQuest.goals[i].currentAmount + "/" + MainManager.Instance.currentQuest.goals[i].requiredAmount;
            progressString += "\n";
        }

        progressText.text = progressString;
    }
}
