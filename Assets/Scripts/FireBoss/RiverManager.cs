using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverManager : MonoBehaviour
{
    BucketPickup bucket;

    // Start is called before the first frame update
    void Start()
    {
        bucket = GameObject.Find("Bucket").GetComponent<BucketPickup>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {
            bucket.isNearWater = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            bucket.isNearWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            bucket.isNearWater = false;
        }
    }
}
