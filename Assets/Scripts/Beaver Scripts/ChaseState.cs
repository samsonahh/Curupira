using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public AttackState attackState;
    public ChargeState chargeState;
    public IdleState idleState;
    public bool isInAttackRange;
    public bool outOfChaseRange;
    private PlayerMovement player;
    private GameObject beaver;
    public float attackRange = 2f;
    public float maxChaseDistance = 12f;
    public float distanceFromPlayer;
    public Vector3 playerLagPosition;
    public IEnumerator getLagPosition;
    public float chaseDurationTimer;
    public float delayTimer;
    public Animator animator;
    public float stompTimer;

    public override State RunCurrentState()
    {
        if (delayTimer == 0f)
        {
            chaseDurationTimer += Time.deltaTime;
            Quaternion targetRotation = Quaternion.LookRotation(playerLagPosition - beaver.transform.position, Vector3.up);
            beaver.transform.rotation = Quaternion.Lerp(beaver.transform.rotation, targetRotation, Time.deltaTime * 5f);
            beaver.transform.position = Vector3.MoveTowards(beaver.transform.position, playerLagPosition, Time.deltaTime * 2f);
            animator.SetBool("isChasing", true);
            stompTimer += Time.deltaTime;
            //if(stompTimer > 6f/7.7f)
            //{
            //    stompTimer = 0;
            //    CameraShakeManager.Instance.ShakeCamera(2f, 0.1f);
            //}
        }

        if(chaseDurationTimer > 6f)
        {
            delayTimer += Time.deltaTime;
            animator.SetBool("isIdle", true);
            animator.SetBool("isChasing", false);
            if (delayTimer > 1f)
            {
                chargeState.playerLagPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
                chargeState.direction = new Vector3(player.transform.position.x, 0, player.transform.position.z) - beaver.transform.position;
                chargeState.doneWithCharge = false;
                chargeState.chargeTimer = 0f;
                chargeState.chargeVelocity = distanceFromPlayer / 5f;
                chargeState.velocityWithAcceleration = distanceFromPlayer / 5f;
                chargeState.targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, 0, player.transform.position.z) - beaver.transform.position, Vector3.up);
                chargeState.delayTimer = 0f;
                beaver.transform.LookAt(player.transform.position);
                animator.SetBool("isIdle", false);
                return chargeState;
            }
        }

        if (isInAttackRange)
        {
            attackState.delayTimer = 0;
            animator.Play("Attack");
            animator.SetBool("isChasing", false);
            return attackState;
        }
        else if (outOfChaseRange)
        {
            chaseDurationTimer = 0f;
            delayTimer = 0f;
            animator.SetBool("isChasing", false);
            return idleState;
        }
        else
        {
            return this;
        }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        beaver = GameObject.Find("Beaver");
        getLagPosition = GetLagPosition();
        StartCoroutine(getLagPosition);
        animator = GameObject.Find("Beaver").GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, beaver.transform.position);
        isInAttackRange = distanceFromPlayer < attackRange;
        outOfChaseRange = distanceFromPlayer > maxChaseDistance;
    }

    public IEnumerator GetLagPosition()
    {
        while (true)
        {
            playerLagPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);

            yield return new WaitForSeconds(1f);
        }
    }
}
