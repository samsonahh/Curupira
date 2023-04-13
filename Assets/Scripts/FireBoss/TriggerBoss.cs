using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    public GameObject fireBoss;
    public FireIdleState fireIdleState;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            fireBoss.SetActive(true);
            fireIdleState.IsPlayerInRange = true;
        }
    }
}
