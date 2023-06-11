using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using Cinemachine;
using Cinemachine.PostFX;

public class SnakeChaseState : State
{
    public SnakeKillState snakeKillState;
    public SnakeScoutState snakeScoutState;
    public GameObject target;

    GameObject player;
    GameObject snakeHead;
    float distanceFromPlayer;

    public bool isPlayerHidden;

    public CinemachinePostProcessing cinemachinePostProcessing;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(GetSnakeHead());
    }

    public override State RunCurrentState()
    {
        if (MainManager.Instance.canGameBePaused)
        {
            CameraShakeManager.Instance.ShakeCamera(2f, 0.7f);
            target.transform.position = player.transform.position;
        }
        else
        {
            target.transform.position = snakeHead.transform.position;
        }

        if(snakeHead != null)
        {
            distanceFromPlayer = Vector3.Distance(player.transform.position, snakeHead.transform.position);

            if(distanceFromPlayer < 0.5f && MainManager.Instance.canGameBePaused)
            {
                return snakeKillState;
            }
        }

        if (isPlayerHidden)
        {
            PostProcessProfile profile = cinemachinePostProcessing.m_Profile;
            StopAllCoroutines();
            StartCoroutine(FadeToNoVignette(profile));
            snakeScoutState.timer = 0;
            return snakeScoutState;
        }

        return this;
    }

    IEnumerator GetSnakeHead()
    {
        while(snakeHead == null)
        {
            snakeHead = GameObject.Find("Segment 0");
            yield return null;
        }
    }

    IEnumerator FadeToNoVignette(PostProcessProfile profile)
    {
        Vignette vignette;
        profile.TryGetSettings(out vignette);
        float timer = 0f;
        while(timer < 1f)
        {
            timer += Time.deltaTime;
            vignette.intensity.value = (0.6f - timer * 0.6f);
            yield return null;
        }
        vignette.intensity.value = 0;
    }
}
