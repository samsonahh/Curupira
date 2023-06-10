using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingBush : MonoBehaviour
{
    SnakeChaseState snakeChaseState;

    private void Start()
    {
        snakeChaseState = FindObjectOfType<SnakeChaseState>().GetComponent<SnakeChaseState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            snakeChaseState.isPlayerHidden = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            snakeChaseState.isPlayerHidden = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            snakeChaseState.isPlayerHidden = false;
        }
    }
}
