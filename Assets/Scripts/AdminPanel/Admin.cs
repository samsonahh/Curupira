using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Admin : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToScene("Main");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToScene("Tree");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ToScene("River");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ToScene("Beaver");
        }
    }

    public void ToScene(string sceneName)
    {
        MainManager.Instance.previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
