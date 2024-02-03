using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaverSceneManager : MonoBehaviour
{
    public GameObject water, waterFlow;

    public GameObject beaver, wall, trigger, playerHealth, boundary;

    void Start()
    {
        
    }

    void Update()
    {
        if (MainManager.Instance.isBeaverDefeated)
        {
            water.SetActive(false);
            waterFlow.SetActive(true);
            beaver.SetActive(false);
            wall.SetActive(false);
            trigger.SetActive(false);
            playerHealth.SetActive(false);
            boundary.SetActive(true);
        }
        else
        {
            water.SetActive(true);
            waterFlow.SetActive(false);
        }
    }
}
