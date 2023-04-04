using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectCollector : MonoBehaviour
{
    private PlayerManager playerManager;
    private Canvas canvas;
    private Camera mainCamera;
    Slider slider;
    TMP_Text infoText;
    float timer;
    float timerMax = 2f;
    public bool onTop = false;
    public float distanceFromObject;

    public int mainQuestIndex;
    public string objectName;


    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        slider = GetComponentInChildren<Slider>();
        infoText = GetComponentInChildren<TMP_Text>();
        canvas = GetComponentInChildren<Canvas>();
        mainCamera = Camera.main;

        slider.gameObject.SetActive(false);
        infoText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromObject = Vector3.Distance(transform.position, playerManager.transform.position);

        if(MainManager.Instance.mainQuestIndex != this.mainQuestIndex)
        {
            infoText.text = "";
            slider.gameObject.SetActive(false);
            return;
        }

        if (distanceFromObject < 2f)
        {
            if (!onTop)
            {
                infoText.text = objectName;
            }
        }
        else
        {
            infoText.text = "";
        }

        if (onTop)
        {
            infoText.text = "E";
        }
        else
        {
            infoText.text = ""; //hide the text
            slider.gameObject.SetActive(false); //hide the bar
            timer = 0; //reset the bar
            slider.value = 0; //reset the timer
        }

        if (timer > timerMax && Input.GetKey(KeyCode.E) && onTop)
        {
            playerManager.isCollecting = false;
            Destroy(gameObject);
            int index = 0;
            for(int i = 0; i < MainManager.Instance.currentQuest.goals.Length; i++)
            {
                if(MainManager.Instance.currentQuest.goals[i].objectName == objectName)
                {
                    index = i;
                }
            }
            MainManager.Instance.currentQuest.goals[index].currentAmount++;
        }
        else if (Input.GetKey(KeyCode.E) && onTop)
        {
            slider.gameObject.SetActive(true);
            timer += Time.deltaTime; // start the timer
            slider.value = (timer / timerMax); // increase the bar by time/Max
            playerManager.isCollecting = true;
        }
        else if (Input.GetKeyUp(KeyCode.E) && onTop)
        {
            slider.gameObject.SetActive(false);
            timer = 0;
            slider.value = 0;
            playerManager.isCollecting = false;
        }
    }

    private void LateUpdate()
    {
        canvas.transform.LookAt(mainCamera.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            onTop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onTop = false;
        }
    }
}