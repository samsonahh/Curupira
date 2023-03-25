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
        GameObject.Find("Button").transform.localScale = new Vector3(size, size, size);
    }

    public void Setting()
    {
        MainManager.Instance.previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Setting");
    }
}
