using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NestManager : MonoBehaviour
{
    private PlayerManager playerManager;
    public Canvas canvas;
    private Camera mainCamera;
    public Slider slider;
    public TMP_Text infoText;
    float timer;
    float timerMax = 2f;
    public bool onTop = false;
    public float distanceFromObject;

    public string objectName;
    public int nestNum;
    public int nestType;

    public GameObject nest, nestWithBirds;

    public bool canBeCollected = true;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        mainCamera = Camera.main;

        slider.gameObject.SetActive(false);
        infoText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (MainManager.Instance.nestProgress[nestNum] == 0)
        {
            objectName = "Nest";
            nest.SetActive(false);
            nestWithBirds.SetActive(false);
        }

        if (MainManager.Instance.nestProgress[nestNum] == 1)
        {
            objectName = "Feed Birds";
            nest.SetActive(true);
            nestWithBirds.SetActive(false);
            canBeCollected = true;
        }

        if (MainManager.Instance.nestProgress[nestNum] == 2)
        {
            objectName = "Bird Nest";
            nest.SetActive(true);
            nestWithBirds.SetActive(true);
        }

        distanceFromObject = Vector3.Distance(transform.position, playerManager.transform.position);

        if (!IsObjectInCurrentQuest())
        {
            infoText.text = "";
            slider.gameObject.SetActive(false);
            return;
        }

        if (distanceFromObject < 4f)
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

        if (timer > timerMax && Input.GetKey(KeyCode.E) && onTop && canBeCollected)
        {
            playerManager.isCollecting = false;
            int index = 0;
            for (int i = 0; i < MainManager.Instance.currentQuest.goals.Length; i++)
            {
                if (MainManager.Instance.currentQuest.goals[i].objectName == objectName)
                {
                    index = i;
                }
            }
            MainManager.Instance.currentQuest.goals[index].currentAmount++;
            if (MainManager.Instance.nestProgress[nestNum] == 0)
            {
                nest.SetActive(true);
                nestWithBirds.SetActive(false);
                MainManager.Instance.nestProgress[nestNum]++;
                canBeCollected = false;
                return;
            }
            if (MainManager.Instance.nestProgress[nestNum] == 1)
            {
                nest.SetActive(true);
                nestWithBirds.SetActive(true);
                MainManager.Instance.nestProgress[nestNum]++;
                canBeCollected = false;
                return;
            }
        }
        else if (Input.GetKey(KeyCode.E) && onTop && canBeCollected)
        {
            slider.gameObject.SetActive(true);
            timer += Time.deltaTime; // start the timer
            slider.value = (timer / timerMax); // increase the bar by time/Max
            playerManager.isCollecting = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            slider.gameObject.SetActive(false);
            timer = 0;
            slider.value = 0;
            playerManager.isCollecting = false;
        }
    }

    public bool IsObjectInCurrentQuest()
    {
        for (int i = 0; i < MainManager.Instance.currentQuest.goals.Length; i++)
        {
            if (MainManager.Instance.currentQuest.goals[i].objectName == objectName)
            {
                return true;
            }
        }
        return false;
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
