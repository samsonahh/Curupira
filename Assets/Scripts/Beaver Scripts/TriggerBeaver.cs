using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBeaver : MonoBehaviour
{
    public GameObject[] walls;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach(GameObject wall in walls)
            {
                wall.SetActive(true);
            }
        }
    }
}
