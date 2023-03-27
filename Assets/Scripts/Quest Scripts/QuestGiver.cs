using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public PlayerManager player;

    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Button acceptButton;
    public Button continueButton;
    public Button closeButton;

    public int textIndex;
    public bool questOpen;
    bool isTyping;
    bool canvasIsOpened;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        nameText = MainManager.Instance.dialogueCanvas.transform.Find("NameText").GetComponent<TMP_Text>();
        dialogueText = MainManager.Instance.dialogueCanvas.transform.Find("DialogueText").GetComponent<TMP_Text>();
        acceptButton = MainManager.Instance.dialogueCanvas.transform.Find("AcceptButton").GetComponent<Button>();
        continueButton = MainManager.Instance.dialogueCanvas.transform.Find("ContinueButton").GetComponent<Button>();
        closeButton = MainManager.Instance.dialogueCanvas.transform.Find("CancelButton").GetComponent<Button>();

        textIndex = 0;
        questOpen = false;
    }

    public void Update()
    {
        if (questOpen)
        {
            UnlockCursor();
            if (quest.isActive)
            {
                OpenIncompleteDialogue();
            }
            if (!quest.isActive)
            {
                OpenDialogue();
            }
        }
    }

    public void OpenDialogue()
    {
        if (!MainManager.Instance.dialogueCanvasIsOpened) {
            StopAllCoroutines();
            StartCoroutine(MainManager.Instance.OpenDialogueCanvas());
        }
        player.isInteracting = true;

        if (textIndex < quest.sentences.Length-1)
        {
            continueButton.gameObject.SetActive(true);
            acceptButton.gameObject.SetActive(false);
        }

        if (textIndex == quest.sentences.Length - 1)
        {
            continueButton.gameObject.SetActive(false);
            acceptButton.gameObject.SetActive(true);
        }

        nameText.text = "Chief: " + quest.title;
        if (!isTyping)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(quest.sentences[textIndex]));
        }
    }

    public void OpenIncompleteDialogue()
    {
        if (!MainManager.Instance.dialogueCanvasIsOpened)
        {
            StopAllCoroutines();
            StartCoroutine(MainManager.Instance.OpenDialogueCanvas());
        }
        player.isInteracting = true;

        nameText.text = "Chief: " + quest.title;
        if (!isTyping)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(quest.sentences[quest.sentences.Length - 1]));
        }

        continueButton.gameObject.SetActive(false);
        acceptButton.gameObject.SetActive(false);
    }

    public void Continue()
    {
        isTyping = false;
        textIndex++;
    }

    public void AcceptQuest()
    {
        CloseDialogue();
        quest.isActive = true;
        MainManager.Instance.currentQuest = quest;
    }

    public void CloseDialogue()
    {
        isTyping = false;
        if (MainManager.Instance.dialogueCanvasIsOpened)
        {
            StopAllCoroutines();
            StartCoroutine(MainManager.Instance.CloseDialogueCanvas());
        }
        player.isInteracting = false;
        textIndex = 0;
        questOpen = false;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
    }
}
