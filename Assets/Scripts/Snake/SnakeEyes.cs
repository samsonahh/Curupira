using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEyes : MonoBehaviour
{
    SnakeScoutState snakeScoutState;

    public Vector3 dir;
    public float timer = 0f;
    public bool isInArea;

    GameObject snakeZone;

    private void Start()
    {
        snakeZone = GameObject.Find("SnakeZone");
        dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        snakeScoutState = FindObjectOfType<SnakeScoutState>().GetComponent<SnakeScoutState>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        isInArea = IsInArea();

        if(timer > 5f)
        {
            SwitchDirections();
            timer = 0;
        }

        if (!isInArea)
        {
            Respawn();
            SwitchDirections();
            timer = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir.normalized, Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            snakeScoutState.isInsideSnakeEyes = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            snakeScoutState.isInsideSnakeEyes = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            snakeScoutState.isInsideSnakeEyes = false;
        }
    }

    void SwitchDirections()
    {
        dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
    }

    void Respawn()
    {
        transform.position = new Vector3(Random.Range(snakeZone.transform.position.x + snakeZone.transform.localScale.x / 2, snakeZone.transform.position.x - snakeZone.transform.localScale.x / 2), 1.25f, Random.Range(snakeZone.transform.position.z - snakeZone.transform.localScale.z / 2, snakeZone.transform.position.z + snakeZone.transform.localScale.z / 2));
    }

    bool IsInArea()
    {
        if(transform.position.x > snakeZone.transform.position.x + snakeZone.transform.localScale.x / 2)
        {
            return false;
        }
        if (transform.position.x < snakeZone.transform.position.x - snakeZone.transform.localScale.x / 2)
        {
            return false;
        }
        if (transform.position.z > snakeZone.transform.position.z + snakeZone.transform.localScale.z / 2)
        {
            return false;
        }
        if (transform.position.z < snakeZone.transform.position.z - snakeZone.transform.localScale.z / 2)
        {
            return false;
        }
        return true;
    }
}
