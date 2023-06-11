using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeKillState : State
{
    bool coroutineStarted;
    public GameObject target;
    PlayerManager playerManager;

    float t;
    float radius = 4f;

    private void Start()
    {
        target = GameObject.Find("SnakeTarget");
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    public override State RunCurrentState()
    {
        t += 4f * Time.deltaTime;

        playerManager.isKnocked = true;

        target.transform.position = new Vector3(playerManager.transform.position.x + radius * Mathf.Cos(t), playerManager.transform.position.y + t/4, playerManager.transform.position.z + radius * Mathf.Sin(t));

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
