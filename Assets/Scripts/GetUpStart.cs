using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpStart : MonoBehaviour
{
    public PlayerManager player;

    private void Start()
    {
        if(MainManager.Instance.previousScene == "Menu")
        {
            StartCoroutine(GetUp());
        }
    }

    IEnumerator GetUp()
    {
        player.isGettingUp = true;

        yield return new WaitForSeconds(11.4f);

        player.isGettingUp = false;
    }
}
