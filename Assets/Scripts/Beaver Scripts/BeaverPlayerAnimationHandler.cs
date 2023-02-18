using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaverPlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private BeaverPlayerMovement playerMovement;
    private BeaverPlayerManager playerManager;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<BeaverPlayerMovement>();
        playerManager = GetComponent<BeaverPlayerManager>();
    }
    void Start()
    {

    }

    private void HandleMovementAnimations()
    {
        animator.SetFloat("MoveX", playerMovement.moveDirection.x);
        animator.SetFloat("MoveZ", playerMovement.moveDirection.z);
        animator.SetBool("isSprinting", playerManager.isSprinting);
        animator.SetBool("isCrouching", playerManager.isCrouching);
    }

    private void HandleJumpAnimation()
    {
        animator.SetBool("isGrounded", playerManager.isGrounded);
        animator.SetBool("isFalling", playerManager.isFalling);
        animator.SetBool("isMoving", playerManager.isMoving);
        animator.SetBool("isJumping", playerManager.isJumping);
    }

    void HandleCollecting()
    {
        animator.SetBool("isCollecting", playerManager.isCollecting);
    }

    void HandleKnocked()
    {
        animator.SetBool("isKnocked", playerManager.isKnocked);
        if (playerManager.isKnocked)
        {
            animator.Play("Knocked");
        }
    }

    void Update()
    {
        HandleJumpAnimation();
        HandleCollecting();
        HandleKnocked();
        HandleMovementAnimations();
        if (playerManager.isInteracting)
        {
            animator.Play("Idle");
        }
    }
}