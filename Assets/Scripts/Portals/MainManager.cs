using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    [Header("Scene Management")]
    public string previousScene = "";

    [Header("Dialogue Management")]
    public GameObject dialogueCanvas;
    public bool dialogueCanvasIsOpened;

    [Header("Quest Management")]
    public Quest[] mainQuests;
    public Quest currentQuest;
    public int mainQuestIndex = 0;

    [Header("Pause Menu")]
    public GameObject pauseCanvas;
    public bool isGamePaused;
    public bool canGameBePaused = true;

    [Header("Fade Canvas")]
    public GameObject fadeCanvas;

    [Header("Settings Canvas")]
    public GameObject settingsCanvas;
    public GameObject colorBlindController;

    [Header("Path Blockers")]
    public bool isBeaverBlocked = true;
    public bool isFireBlocked = true;

    [Header("Nest Rememberer")]
    public int[] nestProgress;

    [Header("Beaver Rememberer")]
    public bool isBeaverDefeated = false;

    [Header("Admin panel")]
    public GameObject adminPanel;
    public bool Admin = false;

    public bool isQuestActive()
    {
        return currentQuest.isActive;
    }
    
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.activeSceneChanged += GetQuestGiver;
        SceneManager.activeSceneChanged += UnFadeCanvas;

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SetupDialogueCanvas();
        SetupPauseCanvas();
        SetupFadeCanvas();
        SetupSettingsCanvas();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isGamePaused = !isGamePaused;
            }
            HandlePause();
        }

        HandleAdminPanel();
    }

    void HandleAdminPanel()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Admin = !Admin;
        }

        adminPanel.SetActive(Admin);
    }

    private void UnFadeCanvas(Scene current, Scene next)
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            StartCoroutine(FadeIn());
        }
    }

    private void GetQuestGiver(Scene current, Scene next)
    {
        QuestGiver sceneQuestGiver = FindObjectOfType<QuestGiver>();

        if (sceneQuestGiver)
        {
            //Grab dialogue components
            TMP_Text nameText = dialogueCanvas.transform.Find("NameText").GetComponent<TMP_Text>();
            TMP_Text dialogueText = dialogueCanvas.transform.Find("DialogueText").GetComponent<TMP_Text>();
            Button acceptButton = dialogueCanvas.transform.Find("AcceptButton").GetComponent<Button>();
            Button continueButton = dialogueCanvas.transform.Find("ContinueButton").GetComponent<Button>();
            Button closeButton = dialogueCanvas.transform.Find("CancelButton").GetComponent<Button>(); 

            acceptButton.onClick.AddListener(sceneQuestGiver.AcceptQuest);
            continueButton.onClick.AddListener(sceneQuestGiver.Continue);
            closeButton.onClick.AddListener(sceneQuestGiver.CloseDialogue);
        }
    }

    void SetupFadeCanvas()
    {
        DontDestroyOnLoad(fadeCanvas.gameObject);
    }

    void SetupSettingsCanvas()
    {
        DontDestroyOnLoad(settingsCanvas.gameObject);
        DontDestroyOnLoad(colorBlindController);
    }

    void SetupDialogueCanvas()
    {
        DontDestroyOnLoad(dialogueCanvas.gameObject);
        dialogueCanvas.gameObject.SetActive(false);
        foreach (Transform o in dialogueCanvas.transform)
        {
            o.gameObject.LeanAlpha(0, 0.1f);
            o.localScale = Vector2.zero;
        }

        Button acceptButton = dialogueCanvas.transform.Find("AcceptButton").GetComponent<Button>();
        Button continueButton = dialogueCanvas.transform.Find("ContinueButton").GetComponent<Button>();
        Button closeButton = dialogueCanvas.transform.Find("CancelButton").GetComponent<Button>();
        Button[] buttons = { acceptButton, continueButton, closeButton };

        foreach (Button button in buttons)
        {
            float hoverScale = 1.1f;

            if(button.name == "CancelButton")
            {
                hoverScale = 1.3f;
            }

            EventTrigger trigger = button.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();

            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => { HoverButton(button, hoverScale); });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((eventData) => { HoverButton(button, 1f); });
            trigger.triggers.Add(entry);
        }
    }

    void SetupPauseCanvas()
    {
        DontDestroyOnLoad(pauseCanvas.gameObject);
        pauseCanvas.gameObject.SetActive(false);
    }
    void HandlePause()
    {
        if (isGamePaused && canGameBePaused)
        {
            PauseGame();
        }
        else
        {
            UnPauseGame();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnPauseGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        pauseCanvas.transform.Find("ConfirmPanel").gameObject.SetActive(false);

        pauseCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResetGame()
    {
        previousScene = "Menu";
        UnPauseGame();
        SceneManager.LoadScene("Main");
        isBeaverBlocked = true;
        isFireBlocked = true;
        isBeaverDefeated = false;

        nestProgress = new int[3];
        mainQuestIndex = -1;
        currentQuest.isActive = true;
        currentQuest.title = "Introduction";
        currentQuest.description = "Follow the dirt road into the giant tree. The Macaw Tribe Leader awaits you.";
        currentQuest.sentences = new string[0];
        currentQuest.goals = new QuestGoal[0];
    }

    public void QuitGame()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void HoverButton(Button button, float size)
    {
        button.transform.LeanScale(new Vector2(size, size), 0.5f).setEaseOutQuart();
    }

    public IEnumerator OpenDialogueCanvas()
    {
        dialogueCanvasIsOpened = true;
        dialogueCanvas.SetActive(true);
        foreach(Transform o in dialogueCanvas.transform)
        {
            o.gameObject.LeanAlpha(1, 0.75f);
            o.LeanScale(Vector2.one, 0.75f).setEaseOutQuart();
        }
        yield return null;
    }

    public IEnumerator CloseDialogueCanvas()
    {
        dialogueCanvasIsOpened = false;
        foreach (Transform o in dialogueCanvas.transform)
        {
            o.gameObject.LeanAlpha(0, 0.25f);
            if(o.name == "CancelButton")
            {
                o.LeanScale(Vector2.zero, 0.05f).setEaseInQuart();
            }
            else
            {
                o.LeanScale(Vector2.zero, 0.25f).setEaseInQuart();
            }
        }
        yield return new WaitForSeconds(0.25f);
        dialogueCanvas.SetActive(false);
    }

    IEnumerator FadeIn()
    {
        fadeCanvas.SetActive(true);
        fadeCanvas.GetComponentInChildren<CanvasRenderer>().SetAlpha(1f);

        canGameBePaused = false;

        yield return new WaitForSeconds(0.5f);

        float timer = 0f;

        while (timer < 1f)
        {
            fadeCanvas.GetComponentInChildren<CanvasRenderer>().SetAlpha(Mathf.MoveTowards(fadeCanvas.GetComponentInChildren<CanvasRenderer>().GetAlpha(), 0f, Time.deltaTime));
            timer += Time.deltaTime;
            yield return null;
        }

        canGameBePaused = true;
        fadeCanvas.SetActive(false);
    }

    public void HoverOverBack(float size)
    {
        settingsCanvas.transform.Find("BackButton").transform.LeanScale(new Vector2(size, size), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }

    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
    }

    public void CloseSettings()
    {
       settingsCanvas.SetActive(false);
    }
}
