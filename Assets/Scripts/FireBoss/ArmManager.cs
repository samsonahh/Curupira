using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmManager : MonoBehaviour
{
    FireBossManager fireBossManager;
    // Start is called before the first frame update
    void Start()
    {
        fireBossManager = GameObject.Find("FireBoss").GetComponent<FireBossManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            fireBossManager.playerCanWater = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            fireBossManager.playerCanWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            fireBossManager.playerCanWater = false;
        }
    }
}
