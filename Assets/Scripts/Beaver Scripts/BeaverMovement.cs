using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaverMovement : MonoBehaviour
{

    private Transform playerTransform;
    private PlayerMovement playerMovement;
    private BeaverPlayerManager playerManager;
    private Vector3 playerLagPosition;
    public float chargeVelocity = 0;
    public float chargeTime = 5f;
    public float rotationSpeed = 4f;
    public IEnumerator lagPos;
    public bool isCharging;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<BeaverPlayerManager>();
        lagPos = LagPos();
        StartCoroutine(lagPos);
    }

    // Update is called once per frame
    void Update()
    {
        chargeVelocity += 5*Time.deltaTime;
        
        if (chargeVelocity > 5 * chargeTime)
        {
            chargeVelocity = 0;
        }
        if (chargeVelocity < 5 * chargeTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerLagPosition.x, 0.5f, playerLagPosition.z), Time.deltaTime * chargeVelocity);
        }

        if(chargeVelocity > 2 * chargeTime)
        {
            isCharging = false;
        }
        else
        {
            isCharging = true;
        }

        Vector3 lookPos = playerLagPosition - transform.position;


        Debug.DrawRay(transform.position, lookPos, Color.red);
        Debug.DrawRay(playerLagPosition, playerTransform.position - playerLagPosition, Color.green);
        Quaternion lookAtRotation = Quaternion.LookRotation(lookPos);

        lookAtRotation = Quaternion.Euler(0, lookAtRotation.eulerAngles.y, 0);

        transform.LookAt(new Vector3(playerLagPosition.x, 0.5f, playerLagPosition.z));

    }

    public IEnumerator LagPos()
    {
        while (true)
        {

            playerLagPosition = playerTransform.position;

            playerLagPosition = transform.position + ((playerLagPosition - transform.position).normalized * 1.25f * Vector3.Distance(playerLagPosition, transform.position));

            yield return new WaitForSeconds(chargeTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (isCharging)
            {
                BeaverPlayerManager pmScript = other.GetComponent<BeaverPlayerManager>();
                Vector3 launchDirection = playerManager.transform.position - (new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.75f));
                playerMovement.controller.Move(3f*launchDirection.normalized);
                playerManager.isKnocked = true;
                playerMovement.knockedTimer = 0;
                pmScript.hp--;
            }
        }
    }
}