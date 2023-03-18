using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2State : State
{
    public Phase3State phase3State;

    PlayerManager player;
    FireBossManager fireManager;

    bool coroutineStarted = false;

    public GameObject wavePrefab;
    public GameObject fireBallPrefab;
    public GameObject fireArm;
    public GameObject fireArmSpot;

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
            StartCoroutine(Phase2());
        }

        if (fireManager.currentHealth <= 30f)
        {
            coroutineStarted = false;
            fireManager.IsVulnerable = false;
            Destroy(arm);
            return phase3State;
        }

        return this;
    }

    IEnumerator Phase2()
    {
        coroutineStarted = true;

        yield return new WaitForSeconds(3f);

        while (coroutineStarted)
        {
            fireManager.IsVulnerable = false;

            // Send a shockwave

            for (int fireballCount = 0; fireballCount < 6; fireballCount++)
            {
                Instantiate(fireBallPrefab, new Vector3(transform.position.x + Random.Range(-7.5f, 7.5f), transform.position.y + Random.Range(3f, 9f), transform.position.z), Quaternion.identity);
                if (fireballCount % 3 == 0)
                {
                    Instantiate(wavePrefab, transform.position, Quaternion.identity);
                }

                yield return new WaitForSeconds(1.5f);
            }

            Instantiate(wavePrefab, transform.position, Quaternion.identity);

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
        }
        
    }
}
