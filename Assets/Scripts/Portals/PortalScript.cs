using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public string targetScene;
    public float timer;
    public float maxTime;
    public MainManager mainManager;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        maxTime = 1f;
        mainManager = MainManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetScene == "Beaver" && MainManager.Instance.currentQuest.title != "Time to Give a Dam")
        {
            return;
        }
        if (targetScene == "River" && MainManager.Instance.currentQuest.title != "Take Out the Flames")
        {
            return;
        }

        if (timer > maxTime)
        {
            Debug.Log("Scene changed");
            timer = 0;

            mainManager.previousScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(targetScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            timer = 0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("Going to " + targetScene + " " + timer);
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            timer = 0f;
        }
    }
}
