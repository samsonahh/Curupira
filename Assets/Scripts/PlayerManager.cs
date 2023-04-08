using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
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
    public bool isHolding;
    public bool isDumping;
    public bool isGettingUp;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GameObject.Find("CM vcam1");
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementConflicts();
        if (MainManager.Instance.isGamePaused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            if (isInteracting)
            {
                virtualCamera.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                virtualCamera.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void HandleMovementConflicts()
    {
        if (isHolding)
        {
            isSprinting = false;
            isCrouching = false;
        }
    }
}
