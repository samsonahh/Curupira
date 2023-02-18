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
            progressText.text = MainManager.Instance.currentQuest.goal.currentAmount + "/" + MainManager.Instance.currentQuest.goal.requiredAmount;
        }
        else
        {
            questTracker.SetActive(false);
        }
    }
}
