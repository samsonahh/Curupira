using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using Cinemachine;
using Cinemachine.PostFX;

public class PortalScript : MonoBehaviour
{
    public string targetScene;
    public MainManager mainManager;
    public bool touching;
    public Image fadeCanvas;

    bool coroutineStarted;

    public CinemachinePostProcessing cinemachinePostProcessing;

    float delayTimer;

    // Start is called before the first frame update
    void Start()
    {
        cinemachinePostProcessing = FindObjectOfType<CinemachinePostProcessing>();
        mainManager = MainManager.Instance;
        fadeCanvas = mainManager.fadeCanvas.GetComponentInChildren<Image>();

        delayTimer = 0f;

        Vignette vignette;
        PostProcessProfile profile = cinemachinePostProcessing.m_Profile;
        profile.TryGetSettings(out vignette);
        vignette.intensity.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        delayTimer += Time.deltaTime;

        if (touching && !coroutineStarted && delayTimer > 2)
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

        mainManager.previousScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(targetScene);

        coroutineStarted = false;
        mainManager.canGameBePaused = true;
    }
}
