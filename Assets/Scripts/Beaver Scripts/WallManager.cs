using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallManager : MonoBehaviour
{
    public Slider hpBar;
    public float hp;


    // Start is called before the first frame update
    void Start()
    {
        hpBar = GameObject.Find("Wall").GetComponentInChildren<Slider>();
        hpBar.value = 1;
        hp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = hp;

        if(hp < 0)
        {
            if(MainManager.Instance.mainQuestIndex == 2)
            {
                Destroy(GameObject.Find("Beaver"));
                MainManager.Instance.currentQuest.goal.currentAmount[0]++;
                Destroy(gameObject);
            }
        }
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Body")
        {
            if (beaver.GetComponent<StateManager>().currentState == chargeState)
            {
                hp -= chargeState.chargeVelocity / 5f;
            }
        }
    }*/
}
