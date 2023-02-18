using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    public ChaseState chaseState;
    public AttackState attackState;
    private BeaverPlayerMovement player;
    private GameObject beaver;
    private BeaverPlayerManager playerManager;
    public bool doneWithCharge;
    public float chargeTimer;
    public float chargeVelocity;
    public Vector3 direction;
    public Vector3 playerLagPosition;
    public Quaternion targetRotation;
    public float distanceFromPlayer;
    public float velocityWithAcceleration;
    public float delayTimer;
    public Animator animator;


    public override State RunCurrentState()
    {
        animator.SetBool("isCharging", true);
        //beaver.transform.rotation = Quaternion.Lerp(beaver.transform.rotation, Quaternion.AngleAxis(targetRotation.eulerAngles.y, Vector3.up), Time.deltaTime * 10f);

        chargeTimer += Time.deltaTime;
        velocityWithAcceleration = 1.5f*chargeVelocity - (chargeVelocity/2f * chargeTimer);
        //chargeVelocity -= 2f * Time.deltaTime;
        beaver.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        beaver.GetComponent<Rigidbody>().AddForce(direction.normalized * velocityWithAcceleration);

        if(distanceFromPlayer < 1.5f)
        {
            direction = Vector3.zero;
            playerLagPosition = Vector3.zero;
            //GameObject.Find("Eyes").transform.localRotation = Quaternion.identity;
            beaver.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            chargeVelocity = 0f;
            attackState.delayTimer = 0;
            animator.Play("ChargeAttack");
            animator.SetBool("isCharging", false);
            CameraShakeManager.Instance.ShakeCamera(5f, 0.15f);
            return attackState;
        }
        //beaver.transform.position = Vector3.MoveTowards(beaver.transform.position, playerLagPosition, chargeVelocity * Time.deltaTime);

        if (doneWithCharge)
        {
            delayTimer += Time.deltaTime;
            animator.SetBool("isCharging", false);
            animator.SetBool("isIdle", true);
            if (delayTimer > 1f)
            {
                chaseState.chaseDurationTimer = 0f;
                chaseState.delayTimer = 0f;
                animator.SetBool("isIdle", false);
                return chaseState;
            }
        }
        return this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BeaverPlayerMovement>();
        beaver = GameObject.Find("Beaver");
        playerManager = player.GetComponent<BeaverPlayerManager>();
        animator = GameObject.Find("Beaver").GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, beaver.transform.position);
        if (chargeTimer > 2f)
        {
            doneWithCharge = true;
            direction = Vector3.zero;
            playerLagPosition = Vector3.zero;
            //GameObject.Find("Eyes").transform.localRotation = Quaternion.identity;
            beaver.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            chargeVelocity = 0f;
        }
    }
}
