using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowArrowScript : MonoBehaviour
{
    public Transform player;

    public Transform chief;
    public Transform fireBoss;
    public Transform beaver;

    private void Update()
    {
        if(MainManager.Instance.mainQuestIndex == -1)
        {
            transform.localPosition = new Vector3(0, 2, 0);
            transform.LookAt(chief);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            return;
        }

        if (MainManager.Instance.mainQuestIndex == 0 && MainManager.Instance.currentQuest.isQuestFullyComplete())
        {
            transform.localPosition = new Vector3(0, 2, 0);
            transform.LookAt(chief);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            return;
        }

        if (MainManager.Instance.mainQuestIndex == 1)
        {
            transform.localPosition = new Vector3(0, 2, 0);
            transform.LookAt(chief);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            return;
        }

        if (MainManager.Instance.mainQuestIndex == 2 && MainManager.Instance.currentQuest.isQuestFullyComplete())
        {
            transform.localPosition = new Vector3(0, 2, 0);
            transform.LookAt(chief);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            return;
        }

        if (MainManager.Instance.mainQuestIndex == 3)
        {
            transform.localPosition = new Vector3(0, 2, 0);
            transform.LookAt(chief);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            return;
        }

        if (MainManager.Instance.mainQuestIndex == 4 && !MainManager.Instance.currentQuest.isQuestFullyComplete())
        {
            transform.localPosition = new Vector3(0, 2, 0);
            transform.LookAt(beaver);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            return;
        }

        if (MainManager.Instance.mainQuestIndex == 4 && MainManager.Instance.currentQuest.isQuestFullyComplete())
        {
            transform.localPosition = new Vector3(0, 2, 0);
            transform.LookAt(chief);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            return;
        }

        if (MainManager.Instance.mainQuestIndex == 5)
        {
            transform.localPosition = new Vector3(0, 2, 0);
            transform.LookAt(fireBoss);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            return;
        }

        transform.localPosition = new Vector3(-100000, -10000, -10000);
    }
}
