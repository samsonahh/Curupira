using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFireMinionCollide : MonoBehaviour
{
    PlayerManager pmScript;
    BucketPickup bucketManager;
    PlayerMovement playerMovement;


    // Start is called before the first frame update
    void Start()
    {
        pmScript = GameObject.Find("Player").GetComponent<PlayerManager>();
        bucketManager = GameObject.Find("Bucket").GetComponent<BucketPickup>();
        playerMovement = pmScript.gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (pmScript.isKnocked)
            {
                return;
            }

            CameraShakeManager.Instance.ShakeCamera(5f, 0.15f);

            bucketManager.PutDownBucket();
            if (pmScript.isHolding)
            {
                bucketManager.fillLevel -= 0.6f;
            }

            pmScript.isKnocked = true;
            playerMovement.knockedTimer = 0f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (pmScript.isKnocked)
            {
                return;
            }

            CameraShakeManager.Instance.ShakeCamera(5f, 0.15f);

            bucketManager.PutDownBucket();
            if (pmScript.isHolding)
            {
                bucketManager.fillLevel = 0f;
            }

            pmScript.isKnocked = true;
            playerMovement.knockedTimer = 0f;
        }
    }
}
