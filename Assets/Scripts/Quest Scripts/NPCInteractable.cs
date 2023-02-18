using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteractable : MonoBehaviour
{
    private GameObject player;
    private PlayerManager playerManager;

    public Quest[] mainQuests;
    public QuestGiver questGiver;

    public float distanceFromPlayer;
    public bool isInteractable;

    //UI
    public Canvas canvas;
    public TMP_Text nameText;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();

        canvas = GetComponentInChildren<Canvas>();
        nameText = canvas.GetComponentInChildren<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.SetActive(false);
        nameText.text = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        //currentQuest = MainManager.Instance.currentQuest;

        HandleNameText();

        if(Input.GetKeyDown(KeyCode.E) && isInteractable)
        {
            if (!MainManager.Instance.currentQuest.isActive)
            {
                questGiver.quest = mainQuests[MainManager.Instance.mainQuestIndex];
                questGiver.questOpen = true;
            }
            if(MainManager.Instance.currentQuest.isActive && MainManager.Instance.currentQuest.goal.isReached())
            {
                MainManager.Instance.currentQuest.Complete();
                MainManager.Instance.mainQuestIndex++;
                questGiver.quest = mainQuests[MainManager.Instance.mainQuestIndex];
                questGiver.questOpen = true;
            }
        }


    }

    void HandleNameText()
    {
        if (distanceFromPlayer > 5f)
        {
            canvas.gameObject.SetActive(false);
            isInteractable = false;
        }

        if (distanceFromPlayer < 5f && distanceFromPlayer > 0.75f)
        {
            canvas.gameObject.SetActive(true);
            nameText.text = gameObject.name;
            isInteractable = false;
        }
        if (distanceFromPlayer < 0.75f)
        {
            nameText.text = "E";
            isInteractable = true;
        }
    }
}
