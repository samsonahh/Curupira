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
        StartCoroutine(PlayGame());
    }

    IEnumerator PlayGame()
    {
        MainManager.Instance.fadeCanvas.SetActive(true);
        MainManager.Instance.canGameBePaused = false;
        MainManager.Instance.fadeCanvas.GetComponentInChildren<CanvasRenderer>().SetAlpha(0f);

        float timer = 0f;

        while (timer < 2f)
        {
            MainManager.Instance.fadeCanvas.GetComponentInChildren<CanvasRenderer>().SetAlpha(Mathf.MoveTowards(MainManager.Instance.fadeCanvas.GetComponentInChildren<CanvasRenderer>().GetAlpha(), 1f, Time.deltaTime / 2));
            timer += Time.deltaTime;
            yield return null;
        }

        MainManager.Instance.previousScene = "Menu";

        SceneManager.LoadScene("Cutscene1");
    }

    public void HoverOverPlay(float size)
    {
        GameObject.Find("PlayButton").transform.LeanScale(new Vector2(size, size), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }

    public void HoverOverSettings(float size)
    {
        GameObject.Find("SettingsButton").transform.LeanScale(new Vector2(size, size), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }
}
