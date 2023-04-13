using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Phase3State : State
{
    PlayerManager player;
    FireBossManager fireManager;

    bool coroutineStarted = false;
    bool cutsceneCoroutineStarted = false;

    public GameObject fireBallPrefab;
    public GameObject smartFireBallPrefab;
    public GameObject fireArm;
    public GameObject fireArmSpot;
    public GameObject smallFireMinion;
    public GameObject wavePrefab;
    public GameObject bigFireMinion;
    public GameObject smoke;

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
            if (!cutsceneCoroutineStarted)
            {
                cutsceneCoroutineStarted = true;
                StopAllCoroutines();
                StartCoroutine(QueueCutscene());
            }
            Destroy(arm);
        }

        return this;
    }

    IEnumerator QueueCutscene()
    {
        MainManager.Instance.fadeCanvas.SetActive(true);
        MainManager.Instance.canGameBePaused = false;
        MainManager.Instance.fadeCanvas.GetComponentInChildren<CanvasRenderer>().SetAlpha(0f);

        float timer = 0f;

        while (timer < 1f)
        {
            MainManager.Instance.fadeCanvas.GetComponentInChildren<CanvasRenderer>().SetAlpha(Mathf.MoveTowards(MainManager.Instance.fadeCanvas.GetComponentInChildren<CanvasRenderer>().GetAlpha(), 1f, Time.deltaTime));
            timer += Time.deltaTime;
            yield return null;
        }

        if(MainManager.Instance.mainQuestIndex == 5)
        {
            MainManager.Instance.currentQuest.goals[0].currentAmount++;
        }

        SceneManager.LoadScene("EndCutscene");
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
                Vector3 spawnPos = new Vector3(2.5f - minionCount * 5f, 0, 10f);

                Instantiate(smoke, spawnPos, Quaternion.identity);

                yield return new WaitForSeconds(0.5f);
                smallMinions[minionCount] = Instantiate(smallFireMinion, spawnPos, Quaternion.identity);

                yield return new WaitForSeconds(2f);
            }

            yield return new WaitForSeconds(7.5f);

            // shoot 10 fireballs
            for (int fireballCount = 0; fireballCount < 10; fireballCount++)
            {
                if (fireballCount % 2 == 1)
                {
                    Instantiate(wavePrefab, transform.position, Quaternion.identity);
                }

                Instantiate(fireBallPrefab, new Vector3(transform.position.x + Random.Range(-7.5f, 7.5f), transform.position.y + Random.Range(3f, 9f), transform.position.z), Quaternion.identity);

                Instantiate(smartFireBallPrefab, new Vector3(transform.position.x + Random.Range(-7.5f, 7.5f), transform.position.y + Random.Range(3f, 9f), transform.position.z), Quaternion.identity);
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

                while (bigMinion != null)
                {
                    healTime += Time.deltaTime;
                    if (healTime > 2f)
                    {
                        healTime = 0f;
                        fireManager.currentHealth += 1;
                    }

                    if (fireManager.currentHealth >= 30)
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
