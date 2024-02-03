using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TriggerBeaver : MonoBehaviour
{
    public GameObject[] walls;

    [SerializeField] private GameObject defaultCam;
    [SerializeField] private GameObject beaverCam;

    PlayerManager playerManager;

    private void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        beaverCam.SetActive(playerManager.isFightingBeaver);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach(GameObject wall in walls)
            {
                wall.SetActive(true);
                playerManager.isFightingBeaver = true;
            }
        }
    }
}
