using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballFollow : MonoBehaviour
{
    GameObject player;
    PlayerManager pmScript;
    PlayerMovement playerMovement;
    Vector3 playerPos;

    BucketPickup bucketManager;

    GameObject spotStart, spotEnd;
    public GameObject spotPrefab;
    public GameObject splashPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pmScript = player.GetComponent<PlayerManager>();
        playerMovement = player.GetComponent<PlayerMovement>();
        bucketManager = GameObject.Find("Bucket").GetComponent<BucketPickup>();

        playerPos = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        spotStart = Instantiate(spotPrefab, playerPos, Quaternion.identity);
        spotEnd = Instantiate(spotPrefab, transform.position, Quaternion.Euler(90, 0, 0));

        playerMovement.launchDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);

        if(transform.position.y <= 0.0001f)
        {
            Destroy(gameObject);
            Destroy(spotStart);
            Destroy(spotEnd);
            CameraShakeManager.Instance.ShakeCamera(5f, 0.15f);
            Instantiate(splashPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (pmScript.isKnocked)
            {
                return;
            }

            CameraShakeManager.Instance.ShakeCamera(5f, 0.15f);
            Destroy(gameObject);
            Destroy(spotStart);
            Destroy(spotEnd);
            Instantiate(splashPrefab, transform.position, Quaternion.identity);

            bucketManager.PutDownBucket();
            if (pmScript.isHolding)
            {
                bucketManager.fillLevel -= 0.6f;
            }

            pmScript.isKnocked = true;
            playerMovement.knockedTimer = 0f;
        }
    }
}
