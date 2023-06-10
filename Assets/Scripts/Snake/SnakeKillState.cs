using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeKillState : State
{
    bool coroutineStarted;

    public override State RunCurrentState()
    {
        GameObject.Find("Player").GetComponent<PlayerManager>().isKnocked = true;
        if (!coroutineStarted)
        {
            coroutineStarted = true;
            StartCoroutine(RestartScene());
        }
        return this;
    }

    IEnumerator RestartScene()
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

        MainManager.Instance.previousScene = "Tree";

        SceneManager.LoadScene("Main");
    }
}
