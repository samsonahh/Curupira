using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3State : State
{
    PlayerManager player;
    FireBossManager fireManager;

    bool coroutineStarted = false;

    public GameObject fireBallPrefab;
    public GameObject fireArm;
    public GameObject fireArmSpot;
    public GameObject smallFireMinion;
    public GameObject wavePrefab;
    public GameObject bigFireMinion;

    GameObject arm;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        fireManager = GameObject.Find("FireBoss").GetComponent<FireBossManager>();
    }

    public override State RunCurrentState()
    {
        if (!coroutineStarted)
        {
            StartCoroutine(Phase3());
        }

        if(fireManager.currentHealth <= 0)
        {
            coroutineStarted = false;
            Destroy(arm);
            Destroy(fireManager.gameObject);
        }

        return this;
    }

    IEnumerator Phase3()
    {
        coroutineStarted = true;
        int cycle = 0;

        yield return new WaitForSeconds(3f);

        while (coroutineStarted)
        {
            fireManager.IsVulnerable = false;

            GameObject[] smallMinions = new GameObject[2];

            for (int minionCount = 0; minionCount < 2; minionCount++)
            {
                smallMinions[minionCount] = Instantiate(smallFireMinion, new Vector3(2.5f - minionCount * 5f, 0, 10f), Quaternion.identity);

                yield return new WaitForSeconds(2f);
            }

            foreach (GameObject minion in smallMinions)
            {
                while (minion != null)
                {
                    yield return null;
                }
            }

            yield return new WaitForSeconds(2f);

            // do a 90% swipe

            yield return new WaitForSeconds(2f);

            // shoot 10 fireballs
            for (int fireballCount = 0; fireballCount < 10; fireballCount++)
            {
                Instantiate(wavePrefab, transform.position, Quaternion.identity);
                Instantiate(fireBallPrefab, new Vector3(transform.position.x + Random.Range(-5f, 5f), transform.position.y + Random.Range(3f, 6f), transform.position.z), Quaternion.identity);
                Instantiate(fireBallPrefab, new Vector3(transform.position.x + Random.Range(-5f, 5f), transform.position.y + Random.Range(3f, 6f), transform.position.z), Quaternion.identity);

                yield return new WaitForSeconds(1.5f);
            }

            yield return new WaitForSeconds(2f);

            if (cycle % 3 == 1)
            {
                // spawn one big minion that tries to jump and slam on player it stuns itself for 2 secs everytime it does
                GameObject bigMinion = Instantiate(bigFireMinion, new Vector3(0, 0, 10f), Quaternion.identity);
                float healTime = 0f;

                // while that minion is alive
                // wait for it to die
                // and heal 1 hp per sec its alive for a max of 20 hp
                // once boss heals 20 hp
                // kill the minion

                while(bigMinion != null)
                {
                    healTime += Time.deltaTime;
                    if(healTime > 1f)
                    {
                        healTime = 0f;
                        fireManager.currentHealth++;
                    }

                    if(fireManager.currentHealth >= 30)
                    {
                        Destroy(bigMinion);
                    }

                    yield return null;
                }

                cycle++;
                yield return new WaitForSeconds(2f);
                continue;
            }

            yield return new WaitForSeconds(2f);

            GameObject spot = Instantiate(fireArmSpot, player.transform.position, Quaternion.identity);
            spot.name = "Spot";
            Vector3 slamSpot = Vector3.zero;

            float timer = 0f;
            while (timer < 2f)
            {
                slamSpot = new Vector3(player.transform.position.x, 0, player.transform.position.z);
                spot.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
                timer += Time.deltaTime;
                yield return null;
            }
            // slam fire arm

            yield return new WaitForSeconds(1f);

            arm = Instantiate(fireArm, new Vector3(slamSpot.x, 10, slamSpot.z), Quaternion.identity);

            yield return new WaitForSeconds(3f);
            Destroy(spot);

            yield return new WaitForSeconds(4f);

            Destroy(arm);
            fireManager.IsVulnerable = false;

            yield return new WaitForSeconds(2f);
            cycle++;
        }

    }
}
