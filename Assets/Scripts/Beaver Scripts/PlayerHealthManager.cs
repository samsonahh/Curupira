using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthText;

    public int playerHealth;
    int maxHealth = 3;
    float healTimer;

    bool coroutineStarted;

    private void Start()
    {
        playerHealth = maxHealth;
        healthText.text = playerHealth + "/" + maxHealth;
        healthSlider.value = playerHealth;
    }

    private void Update()
    {
        ManageUI();
        if (playerHealth < maxHealth && playerHealth != 0)
        {
            healTimer += Time.deltaTime;
        }

        if(healTimer > 7.5f)
        {
            healTimer = 0f;
            if(playerHealth < maxHealth)
            {
                playerHealth++;
            }
        }

        if(playerHealth <= 0)
        {
            GameObject.Find("Player").GetComponent<PlayerManager>().isKnocked = true;
            if (!coroutineStarted)
            {
                coroutineStarted = true;
                StartCoroutine(RestartScene());
            }
        }
    }

    void ManageUI()
    {
        healthText.text = playerHealth + "/" + maxHealth;
        healthSlider.value = playerHealth;
    }

    IEnumerator RestartScene()
    {
        MainManager.Instance.fadeCanvas.SetActive(true);
        MainManager.Instance.canGameBePaused = false;
        MainManager.Instance.fadeCanvas.GetComponentInChildren<CanvasRenderer>().SetAlpha(0f);

        float timer = 0f;

        while (timer < 2f)
        {
            MainManager.Instance.fadeCanvas.GetComponentInChildren<CanvasRenderer>().SetAlpha(Mathf.MoveTowards(MainManager.Instance.fadeCanvas.GetComponentInChildren<CanvasRenderer>().GetAlpha(), 1f, Time.deltaTime/2));
            timer += Time.deltaTime;
            yield return null;
        }

        MainManager.Instance.previousScene = "Main";

        SceneManager.LoadScene("Beaver");
    }
}
