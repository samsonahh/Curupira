using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public ChaseState chaseState;
    private BeaverPlayerMovement player;
    private BeaverPlayerManager playerManager;
    private GameObject beaver;
    public float attackRange = 2.5f;
    public bool isInAttackRange;
    public float distanceFromPlayer;
    public float delayTimer;
    public Animator animator;

    public override State RunCurrentState()
    {
        //if(delayTimer == 0)
        //{
        //    //beaver.transform.Rotate(new Vector3(-90f, 0, 0) * 100f * Time.deltaTime);
        //}

        if (!playerManager.isKnocked)
        {
            player.knockedTimer = 0;
            Vector3 launchDirection = player.transform.position - beaver.transform.position;
            player.launchDirection = new Vector3(3f * launchDirection.x, launchDirection.y, 3f * launchDirection.z);
            playerManager.isKnocked = true;
            playerManager.hp--;
            CameraShakeManager.Instance.ShakeCamera(5f, 0.2f);
        }

        if (!isInAttackRange)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer > 1.5f)
            {
                chaseState.chaseDurationTimer = 0f;
                chaseState.delayTimer = 0f;
                animator.SetBool("isChasing", true);
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
        isInAttackRange = distanceFromPlayer < attackRange;
    }
}
