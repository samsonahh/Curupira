using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFireMinion : MonoBehaviour
{
    GameObject player;
    Vector3 delayedPlayerPos;

    BucketPickup bucketManager;
    PlayerManager pmScript;
    PlayerMovement playerMovement;

    bool getPlayerPos;

    float bombTimer;
    float maxBombTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bucketManager = GameObject.Find("Bucket").GetComponent<BucketPickup>();
        pmScript = player.GetComponent<PlayerManager>();
        playerMovement = player.GetComponent<PlayerMovement>();

        playerMovement.launchDirection = Vector3.zero;

        bombTimer = 0.01f;
        maxBombTimer = 10f;

        getPlayerPos = true;
        StartCoroutine(DelayedPlayerPosition());
    }

    // Update is called once per frame
    void Update()
    {   
        if(bombTimer < maxBombTimer && bombTimer > 0)
        {
            bombTimer += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, delayedPlayerPos, 3f * Time.deltaTime);
            gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.Lerp(Color.black, Color.red, bombTimer/maxBombTimer);
        }

        if (bombTimer > maxBombTimer)
        {
            getPlayerPos = false;
            StartCoroutine(JumpAndExplode());
            bombTimer = 0f;
        }
    }

    IEnumerator DelayedPlayerPosition()
    {
        while (getPlayerPos)
        {
            delayedPlayerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator JumpAndExplode()
    {
        float jumpTimer = 0f;

        while(jumpTimer < 0.25f)
        {
            jumpTimer += Time.deltaTime;
            Vector3 playerDir = (player.transform.position - transform.position).normalized;
            transform.Translate(new Vector3(playerDir.x, 1, playerDir.z) * 10f * Time.deltaTime);
            yield return null;
        }

        GameObject explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        explosion.transform.position = transform.position;
        Destroy(gameObject.transform.GetChild(0).gameObject);

        yield return new WaitForSeconds(0.5f);
        Destroy(explosion);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
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
                bucketManager.fillLevel -= 0.6f;
            }

            pmScript.isKnocked = true;
            playerMovement.knockedTimer = 0f;
            Destroy(gameObject);
        }
    }
}
