using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button()
    {
        MainManager.Instance.previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Main");
    }

    public void HoverOverPlay(float size)
    {
        GameObject.Find("PlayButton").transform.LeanScale(new Vector2(size, size), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }

    public void HoverOverSettings(float size)
    {
        GameObject.Find("SettingsButton").transform.LeanScale(new Vector2(size, size), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }

    public void Setting()
    {
        MainManager.Instance.previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Setting");
    }
}
