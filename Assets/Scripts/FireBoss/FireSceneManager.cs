using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSceneManager : MonoBehaviour
{
    public GameObject fireBossCanvas, fireBoss;
    public FireBossManager fireBossManager;

    public FireIdleState fireIdleState;

    void Start()
    {
    }

    private void Update()
    {
        if (fireIdleState.IsPlayerInRange && fireBoss.activeSelf)
        {
            fireBoss.SetActive(true);
            fireBossManager.enabled = true;
            fireBossCanvas.SetActive(true);
        }
    }

}
