using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public string targetScene;
    public float timer;
    float maxTime;
    public MainManager mainManager;
    public bool touching;

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
        if (touching)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
        }
        if (timer > maxTime)
        {
            timer = 0;

            mainManager.previousScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(targetScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            touching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            touching = false;
        }
    }
}
