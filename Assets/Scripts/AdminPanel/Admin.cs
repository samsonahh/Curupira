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
            ToScene("Spawn");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToScene("Main");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ToScene("Tree");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ToScene("Chief");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ToScene("River");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
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
