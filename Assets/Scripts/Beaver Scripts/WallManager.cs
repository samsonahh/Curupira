using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WallManager : MonoBehaviour
{
    public Slider hpBar;
    public float hp;

    bool coroutineStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        hpBar = GameObject.Find("Wall").GetComponentInChildren<Slider>();
        hpBar.value = 1;
        hp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = hp;

        if(hp < 0)
        {
            if(MainManager.Instance.mainQuestIndex == 4 && !coroutineStarted)
            {
                hp = 0;
                StartCoroutine(QueueCutscene());
                coroutineStarted = true;
            }
        }
    }

    IEnumerator QueueCutscene()
    {
        MainManager.Instance.fadeCanvas.SetActive(true);
        MainManager.Instance.canGameBePaused = false;
        MainManager.Instance.fadeCanvas.GetComponentInChildren<CanvasRenderer>().SetAlpha(0f);

        float timer = 0f;

        while (timer < 1f)
        {
            MainManager.Instance.fadeCanvas.GetComponentInChildren<CanvasRenderer>().SetAlpha(Mathf.MoveTowards(MainManager.Instance.fadeCanvas.GetComponentInChildren<CanvasRenderer>().GetAlpha(), 1f, Time.deltaTime));
            timer += Time.deltaTime;
            yield return null;
        }

        MainManager.Instance.currentQuest.goals[0].currentAmount++;
        MainManager.Instance.isBeaverDefeated = true;

        MainManager.Instance.previousScene = "Main";

        SceneManager.LoadScene("BeaverCutscene");
    }
}
