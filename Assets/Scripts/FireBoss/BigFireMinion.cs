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

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        bucketManager = GameObject.Find("Bucket").GetComponent<BucketPickup>();
        pmScript = GameObject.Find("Player").GetComponent<PlayerManager>();
        playerMovement = pmScript.gameObject.GetComponent<PlayerMovement>();
        anim = GetComponentInChildren<Animator>();

        gameObject.name = "BigMinion";

        StartCoroutine(DelayedPlayerPosition());
    }

    // Update is called once per frame
    void Update()
    {
        spinTimer += Time.deltaTime;

        if(spinTimer > 5f)
        {
            anim.SetBool("isVulnerable", true);
            transform.Find("Body").Find("Indicator").gameObject.SetActive(true);
            if (spinTimer > 9f)
            {
                anim.SetBool("isVulnerable", false);
                transform.Find("Body").Find("Indicator").gameObject.SetActive(false);
                spinTimer = 0f;
            }
            return;
        }

        if(Vector3.Distance(playerMovement.transform.position, transform.position) > 1f && !slamming)
        {
            transform.position = Vector3.MoveTowards(transform.position, delayedPlayerPos, 2f * Time.deltaTime);
        }
        else if(Vector3.Distance(playerMovement.transform.position, transform.position) < 1f)
        {
            slamming = true;
            spinTimer = 0f;
            StartCoroutine(Slam());
        }

        HandleAnimations();

        Quaternion targetRotation = Quaternion.LookRotation(delayedPlayerPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
    }

    void HandleAnimations()
    {
        anim.SetBool("isAttacking", slamming);
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
        pmScript.isCollecting = false;
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
        Vector3 launchDirection = transform.forward;
        playerMovement.launchDirection = new Vector3(3f * launchDirection.x, launchDirection.y, 3f * launchDirection.z);
        playerMovement.launchVelocity = 3f;
        playerMovement.knockedTimer = 0f;

        yield return new WaitForSeconds(2f);
        slamming = false;
    }
}
