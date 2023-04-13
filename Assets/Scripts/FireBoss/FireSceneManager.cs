using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSceneManager : MonoBehaviour
{
    public GameObject fireBossCanvas, fireBoss;
    public FireBossManager fireBossManager;

    FireIdleState fireIdleState;

    void Start()
    {
        fireIdleState = GameObject.Find("FireBoss").GetComponentInChildren<FireIdleState>();
    }

    private void Update()
    {
        if (fireIdleState.IsPlayerInRange && fireBoss.activeSelf)
        {
            fireBossManager.enabled = true;
            fireBossCanvas.SetActive(true);
        }
    }

}
