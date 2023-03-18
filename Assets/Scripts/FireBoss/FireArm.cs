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

    // Start is called before the first frame update
    void Start()
    {
        pmScript = GameObject.Find("Player").GetComponent<PlayerManager>();
        playerMovement = pmScript.gameObject.GetComponent<PlayerMovement>();
        bucketManager = GameObject.Find("Bucket").GetComponent<BucketPickup>();
        fireBossManager = GameObject.Find("FireBoss").GetComponent<FireBossManager>();

        spot = GameObject.Find("Spot");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll)
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (pmScript.isKnocked)
            {
                return;
            }

            CameraShakeManager.Instance.ShakeCamera(7.5f, 0.3f);
            GetComponent<Collider>().isTrigger = true;
            Destroy(spot);

            bucketManager.PutDownBucket();
            if (pmScript.isHolding)
            {
                bucketManager.fillLevel = 0;
            }

            pmScript.isKnocked = true;
            playerMovement.knockedTimer = 0f;
        }
        else if(collision.gameObject.name == "River")
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            CameraShakeManager.Instance.ShakeCamera(5f, 0.15f);
            fireBossManager.IsVulnerable = true;
            Destroy(spot);
            Debug.Log("OUCH!");
        }
        else if(collision.gameObject.name == "Plane")
        {
            CameraShakeManager.Instance.ShakeCamera(7.5f, 0.3f);
            GetComponent<Collider>().isTrigger = true;
            Destroy(spot);
        }
        else if(collision.gameObject.name == "Bucket")
        {
            CameraShakeManager.Instance.ShakeCamera(7.5f, 0.3f);
            GetComponent<Collider>().isTrigger = true;
            Destroy(spot);
        }
        else
        {
            CameraShakeManager.Instance.ShakeCamera(7.5f, 0.3f);
            GetComponent<Collider>().isTrigger = true;
            Destroy(spot);
        }
    }

}
