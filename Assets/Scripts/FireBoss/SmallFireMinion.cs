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

    public GameObject explosion;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bucketManager = GameObject.Find("Bucket").GetComponent<BucketPickup>();
        pmScript = player.GetComponent<PlayerManager>();
        playerMovement = player.GetComponent<PlayerMovement>();
        anim = GetComponentInChildren<Animator>();

        playerMovement.launchDirection = Vector3.zero;

        bombTimer = 0.01f;
        maxBombTimer = 10f;

        getPlayerPos = true;
        StartCoroutine(DelayedPlayerPosition());
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimations();
        if(bombTimer < maxBombTimer && bombTimer > 0)
        {
            bombTimer += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, delayedPlayerPos, 3f * Time.deltaTime);
        }

        if (bombTimer > maxBombTimer)
        {
            getPlayerPos = false;
            StartCoroutine(JumpAndExplode());
            bombTimer = 0f;
        }
    }

    void HandleAnimations()
    {
        anim.SetBool("isMoving", true);
        transform.LookAt(player.transform);
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
            transform.Translate(Vector3.forward * 10f * Time.deltaTime);
            yield return null;
        }
        anim.Play("Jump");
        Instantiate(explosion, transform.position, Quaternion.identity);
        if(Vector3.Distance(player.transform.position, transform.position) < 3f)
        {
            if (!pmScript.isKnocked)
            {
                HitPlayer();
            }
        }
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
            HitPlayer();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
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
            HitPlayer();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void HitPlayer()
    {
        CameraShakeManager.Instance.ShakeCamera(5f, 0.15f);

        bucketManager.PutDownBucket();
        if (pmScript.isHolding)
        {
            bucketManager.fillLevel = 0;
        }

        pmScript.isKnocked = true;
        playerMovement.knockedTimer = 0f;
    }
}
