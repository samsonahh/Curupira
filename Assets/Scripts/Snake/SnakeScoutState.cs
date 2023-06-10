using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using Cinemachine;
using Cinemachine.PostFX;

public class SnakeScoutState : State
{
    public SnakeChaseState snakeChaseState;

    PlayerManager playerManager;
    float timer = 0f;
    float caughtTime = 3f;
    public bool isInArea;

    public bool isInsideSnakeEyes;

    public CinemachinePostProcessing cinemachinePostProcessing;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    public override State RunCurrentState()
    {
        Vignette vignette;
        PostProcessProfile profile = cinemachinePostProcessing.m_Profile;
        profile.TryGetSettings(out vignette);
        vignette.intensity.value = timer * 0.65f / caughtTime;

        if (isInArea)
        {
            if ((playerManager.isCrouching || !playerManager.isMoving) && !isInsideSnakeEyes)
            {
                timer -= Time.deltaTime;

            }
            if (!playerManager.isCrouching && playerManager.isMoving)
            {
                timer += Time.deltaTime;
           
            }
            if(!playerManager.isCrouching && playerManager.isMoving && playerManager.isSprinting)
            {
                timer += 2f * Time.deltaTime;
            }
            if (isInsideSnakeEyes)
            {
                timer += 5f * Time.deltaTime;
            }
            if(timer < 0)
            {
                timer = 0;
            }
        }
        if(timer > caughtTime)
        {
            vignette.intensity.value = 0.65f;
            return snakeChaseState;
        }
        return this;
    }
}
