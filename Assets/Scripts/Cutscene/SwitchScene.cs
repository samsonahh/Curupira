using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    float timer;
    public float sceneDuration;
    public string targetScene;

    private void Start()
    {
        timer = 0f;
    }

    private void Update()
    {

        if(timer > sceneDuration)
        {
            timer = 0f;
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
