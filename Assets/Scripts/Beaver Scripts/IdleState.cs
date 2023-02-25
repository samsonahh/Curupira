using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    public bool canSeeThePlayer;
    private PlayerMovement player;
    private GameObject beaver;
    public float viewDistance = 6f;
    public float distanceFromPlayer;
    Vector3 randomPosition;
    public IEnumerator getRandomPosition;
    public Animator animator;

    public override State RunCurrentState()
    {
        animator.SetBool("isIdle", true);
        //Quaternion targetRotation = Quaternion.LookRotation(randomPosition - beaver.transform.position, Vector3.up);
        //beaver.transform.rotation = Quaternion.Lerp(beaver.transform.rotation, targetRotation, Time.deltaTime * 5f);

        if (canSeeThePlayer)
        {
            chaseState.chaseDurationTimer = 0f;
            animator.SetBool("isIdle", false);
            return chaseState;
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
        getRandomPosition = GetRandomPosition();
        StartCoroutine(getRandomPosition);
        animator = GameObject.Find("Beaver").GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, beaver.transform.position);
        canSeeThePlayer = distanceFromPlayer < viewDistance;
    }

    public IEnumerator GetRandomPosition()
    {
        while (true)
        {
            randomPosition = new Vector3(10f * Random.Range(-1, 1) + 1f, 0, 10f * Random.Range(-1, 1) + 1f);

            yield return new WaitForSeconds(1f);
        }
    }
}
