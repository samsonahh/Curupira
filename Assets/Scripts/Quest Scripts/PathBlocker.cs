using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlocker : MonoBehaviour
{
    public GameObject beaverBlocker, fireBlocker;
    public bool beaverBlockOnTop, fireBlockOnTop;

    public GameObject beaverBlockerCanvas, fireBlockerCanvas;
    bool beaverVis, fireVis;

    private void Start()
    {
        beaverVis = true;
        fireVis = true;
    }

    private void LateUpdate()
    {
        beaverBlockerCanvas.transform.LookAt(Camera.main.transform);
        fireBlockerCanvas.transform.LookAt(Camera.main.transform);
    }

    private void Update()
    {
        beaverBlockerCanvas.SetActive(beaverBlockOnTop && MainManager.Instance.mainQuestIndex == 4 && beaverVis);
        fireBlockerCanvas.SetActive(fireBlockOnTop && MainManager.Instance.mainQuestIndex == 5 && fireVis);

        if (!MainManager.Instance.isBeaverBlocked)
        {
            beaverBlocker.SetActive(false);
        }
        if (!MainManager.Instance.isFireBlocked)
        {
            fireBlocker.SetActive(false);
        }

        if (beaverBlockOnTop && MainManager.Instance.mainQuestIndex == 4)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                beaverVis = false;
                MainManager.Instance.isBeaverBlocked = false;
            }
        }

        if(fireBlockOnTop && MainManager.Instance.mainQuestIndex == 5)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                fireVis = false;
                MainManager.Instance.isFireBlocked = false;
            }
        }
    }
}
