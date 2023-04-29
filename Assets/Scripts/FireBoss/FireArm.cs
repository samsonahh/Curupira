using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArm : MonoBehaviour
{
    PlayerManager pmScript;
    PlayerMovement playerMovement;

    BucketPickup bucketManager;
    FireBossManager fireBossManager;

    GameObject spot;
    public GameObject indicator;

    bool touched = false;

    // Start is called before the first frame update
    void Start()
    {
        pmScript = GameObject.Find("Player").GetComponent<PlayerManager>();
        playerMovement = pmScript.gameObject.GetComponent<PlayerMovement>();
        bucketManager = GameObject.Find("Bucket").GetComponent<BucketPickup>();
        fireBossManager = GameObject.Find("FireBoss").GetComponent<FireBossManager>();

        spot = GameObject.Find("Spot");
    }

    private void Update()
    {
        if(transform.position.y <= 0.00001f + 1.5f && !touched)
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            CameraShakeManager.Instance.ShakeCamera(5f, 0.15f);
            fireBossManager.IsVulnerable = true;
            indicator.SetActive(true);
            Destroy(spot);
            touched = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!touched)
        {
            if(other.tag == "Player")
            {
                if (pmScript.isKnocked)
                {
                    return;
                }

                CameraShakeManager.Instance.ShakeCamera(7.5f, 0.3f);
                Destroy(spot);

                bucketManager.PutDownBucket();
                if (pmScript.isHolding)
                {
                    bucketManager.fillLevel = 0;
                }

                pmScript.isKnocked = true;
                playerMovement.knockedTimer = 0f;
                touched = true;
            }
        }
    }

}
