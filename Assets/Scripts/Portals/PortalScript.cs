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

    // Start is called before the first frame update
    void Start()
    {
        mainManager = MainManager.Instance;
        fadeCanvas = mainManager.fadeCanvas.GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        fadeCanvas.transform.parent.gameObject.SetActive(touching);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            touching = true;
            StopAllCoroutines();
            StartCoroutine(Teleport());
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
        fadeCanvas.color = new Color(0, 0, 0, 0);

        float timer = 0f;
        while(timer < 2f)
        {
            timer += Time.deltaTime;
            fadeCanvas.color = new Color(0, 0, 0, timer/2f);
            yield return null;
        }

        mainManager.previousScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(targetScene);
    }
}
