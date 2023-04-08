using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public ChaseState chaseState;
    private PlayerMovement player;
    private PlayerManager playerManager;
    PlayerHealthManager playerHealthManager;
    private GameObject beaver;
    public float attackRange = 2f;
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
            Vector3 launchDirection = beaver.transform.forward;
            player.launchDirection = new Vector3(3f * launchDirection.x, launchDirection.y, 3f * launchDirection.z);
            player.launchVelocity = 6f;
            playerManager.isKnocked = true;
            CameraShakeManager.Instance.ShakeCamera(5f, 0.2f);
            playerHealthManager.playerHealth--;
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        beaver = GameObject.Find("Beaver");
        playerManager = player.GetComponent<PlayerManager>();
        animator = GameObject.Find("Beaver").GetComponentInChildren<Animator>();
        playerHealthManager = GameObject.Find("PlayerHealthCanvas").GetComponent<PlayerHealthManager>();
    }

    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, beaver.transform.position);
        isInAttackRange = distanceFromPlayer < attackRange;
    }
}
