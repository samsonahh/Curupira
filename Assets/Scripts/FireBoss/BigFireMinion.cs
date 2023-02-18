using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFireMinion : MonoBehaviour
{
    BucketPickup bucketManager;
    PlayerManager pmScript;
    PlayerMovement playerMovement;

    bool slamming;

    Vector3 delayedPlayerPos;

    float spinTimer;

    // Start is called before the first frame update
    void Start()
    {
        bucketManager = GameObject.Find("Bucket").GetComponent<BucketPickup>();
        pmScript = GameObject.Find("Player").GetComponent<PlayerManager>();
        playerMovement = pmScript.gameObject.GetComponent<PlayerMovement>();

        gameObject.name = "BigMinion";

        StartCoroutine(DelayedPlayerPosition());
    }

    // Update is called once per frame
    void Update()
    {
        spinTimer += Time.deltaTime;

        if(spinTimer > 5f)
        {
            if(spinTimer > 7.5f)
            {
                spinTimer = 0f;
            }
            return;
        }

        if(Vector3.Distance(playerMovement.transform.position, transform.position) > 1f && !slamming)
        {
            transform.position = Vector3.MoveTowards(transform.position, delayedPlayerPos, 2f * Time.deltaTime);
            transform.GetChild(0).Rotate(0, 90 * 30 * Time.deltaTime, 0);
        }
        else if(Vector3.Distance(playerMovement.transform.position, transform.position) < 1f)
        {
            slamming = true;
            spinTimer = 0f;
            StartCoroutine(Slam());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            bucketManager.minionNear = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            bucketManager.minionNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            bucketManager.minionNear = false;
        }
    }

    IEnumerator DelayedPlayerPosition()
    {
        while (true)
        {
            delayedPlayerPos = new Vector3(pmScript.transform.position.x, 0, pmScript.transform.position.z);

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Slam()
    {
        if (pmScript.isKnocked)
        {
            yield break;
        }

        CameraShakeManager.Instance.ShakeCamera(5f, 0.15f);

        bucketManager.PutDownBucket();
        if (pmScript.isHolding)
        {
            bucketManager.fillLevel = 0f;
        }

        pmScript.isKnocked = true;
        Vector3 launchDirection = pmScript.transform.position - transform.position;
        playerMovement.launchDirection = new Vector3(3f * launchDirection.x, launchDirection.y, 3f * launchDirection.z);
        playerMovement.knockedTimer = 0f;

        yield return new WaitForSeconds(2f);
        slamming = false;
    }
}
