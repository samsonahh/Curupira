using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverManager : MonoBehaviour
{
    BucketPickup bucket;
    FireIdleState fireIdleState;

    // Start is called before the first frame update
    void Start()
    {
        bucket = GameObject.Find("Bucket").GetComponent<BucketPickup>();
        fireIdleState = GameObject.Find("FireBoss").GetComponentInChildren<FireIdleState>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {
            bucket.isNearWater = true;
            fireIdleState.IsPlayerInRange = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            bucket.isNearWater = true;
            fireIdleState.IsPlayerInRange = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            bucket.isNearWater = false;
            fireIdleState.IsPlayerInRange = true;
        }
    }
}
