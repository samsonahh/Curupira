using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    FireIdleState fireIdleState;

    // Start is called before the first frame update
    void Start()
    {
        fireIdleState = GameObject.Find("FireBoss").GetComponentInChildren<FireIdleState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            fireIdleState.IsPlayerInRange = true;
        }
    }
}
