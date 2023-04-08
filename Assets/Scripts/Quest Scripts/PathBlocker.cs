using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlocker : MonoBehaviour
{
    public GameObject beaverBlocker, fireBlocker;
    public bool beaverBlockOnTop, fireBlockOnTop;

    private void Update()
    {
        if (!MainManager.Instance.isBeaverBlocked)
        {
            beaverBlocker.SetActive(false);
        }
        if (!MainManager.Instance.isFireBlocked)
        {
            beaverBlocker.SetActive(false);
        }

        if (beaverBlockOnTop && MainManager.Instance.mainQuestIndex == 4)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                beaverBlocker.SetActive(false);
                MainManager.Instance.isBeaverBlocked = false;
            }
        }

        if(beaverBlockOnTop && MainManager.Instance.mainQuestIndex == 5)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                fireBlocker.SetActive(false);
                MainManager.Instance.isFireBlocked = false;
            }
        }
    }
}
