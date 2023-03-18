using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BeaverPlayerManager : MonoBehaviour
{
    public int hp;
    public Slider hpBar;
    public TMP_Text hpText;
    public float timer;

    public GameObject virtualCamera;

    //Player Bools
    public bool isGrounded;
    public bool isMoving;
    public bool isJumping;
    public bool isFalling;
    public bool isSprinting;
    public bool isCollecting;
    public bool isCrouching;
    public bool isKnocked;
    public bool isInteracting;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GameObject.Find("CM vcam1");
        hpBar = GameObject.Find("HP").GetComponent<Slider>();
        hpText = GameObject.Find("HPText").GetComponent<TMP_Text>();
        hpText.text = "3/3";
        hpBar.value = 3;
        hp = 3;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = hp;
        hpText.text = hp + "/3";

        if (hp == 3)
        {
            timer = 0;
        }
        if (hp < 0)
        {
            hp = 0;
        }
        if (hp < 3)
        {
            timer += Time.deltaTime;
        }
        if (timer > 10)
        {
            hp++;
            timer = 0;
        }

        if (isInteracting)
        {
            virtualCamera.SetActive(false);
        }
        else
        {
            virtualCamera.SetActive(true);
        }
    }
}
