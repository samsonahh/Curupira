using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeChaseState : State
{
    public SnakeKillState snakeKillState;
    public GameObject target;

    GameObject player;
    GameObject snakeHead;
    float distanceFromPlayer;

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

            if(distanceFromPlayer < 1f && MainManager.Instance.canGameBePaused)
            {
                return snakeKillState;
            }
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
}
