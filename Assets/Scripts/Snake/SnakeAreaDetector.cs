using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAreaDetector : MonoBehaviour
{
    public SnakeScoutState snakeScoutState;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            snakeScoutState.isInArea = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            snakeScoutState.isInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            snakeScoutState.isInArea = false;
        }
    }
}
