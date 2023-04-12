using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public float sceneDuration;
    public string targetScene = "Main";

    private void Start()
    {
        StartCoroutine(WaitForScene());
    }

    IEnumerator WaitForScene()
    {
        yield return new WaitForSeconds(sceneDuration);

        SceneManager.LoadScene(targetScene);
    }
}
