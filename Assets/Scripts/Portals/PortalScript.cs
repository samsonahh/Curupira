using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalScript : MonoBehaviour
{
    public string targetScene;
    public MainManager mainManager;
    public bool touching;
    public Image fadeCanvas;

    bool coroutineStarted;

    // Start is called before the first frame update
    void Start()
    {
        mainManager = MainManager.Instance;
        fadeCanvas = mainManager.fadeCanvas.GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (touching && !coroutineStarted)
        {
            coroutineStarted = true;
            StartCoroutine(Teleport());
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

    IEnumerator Teleport()
    {
        fadeCanvas.transform.parent.gameObject.SetActive(true);
        mainManager.canGameBePaused = false;
        fadeCanvas.GetComponent<CanvasRenderer>().SetAlpha(0f);

        float timer = 0f;

        while(timer < 1f)
        {
            fadeCanvas.GetComponent<CanvasRenderer>().SetAlpha(Mathf.MoveTowards(fadeCanvas.GetComponent<CanvasRenderer>().GetAlpha(), 1f, Time.deltaTime));
            timer += Time.deltaTime;
            yield return null;
        }

        fadeCanvas.transform.parent.gameObject.SetActive(false);

        mainManager.previousScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(targetScene);

        coroutineStarted = false;
        mainManager.canGameBePaused = true;
    }
}
