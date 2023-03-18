using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    GameObject player;
    PlayerManager pmScript;
    PlayerMovement playerMovement;

    BucketPickup bucketManager;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pmScript = player.GetComponent<PlayerManager>();
        playerMovement = player.GetComponent<PlayerMovement>();
        bucketManager = GameObject.Find("Bucket").GetComponent<BucketPickup>();

        CameraShakeManager.Instance.ShakeCamera(5f, 0.15f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * 10f * Time.deltaTime);

        timer += Time.deltaTime;

        if(timer > 10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (pmScript.isKnocked)
            {
                return;
            }

            CameraShakeManager.Instance.ShakeCamera(5f, 0.15f);

            bucketManager.PutDownBucket();
            if (pmScript.isHolding)
            {
                bucketManager.fillLevel -= 0;
            }

            pmScript.isKnocked = true;
            playerMovement.knockedTimer = 0f;
        }
        if(other.name == "River")
        {
            Destroy(gameObject);
        }
    }
}
