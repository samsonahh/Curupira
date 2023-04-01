using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Camera cam;
    //Movement
    public float speed = 3.5f;
    float sprintSpeed = 6f;
    float crouchSpeed = 1.5f;
    public float rotationSmoothTime = 0.1f;
    float currentAngle;
    float currentAngleVelocity;
    //Animation
    public Vector3 moveDirection;
    //Gravity
    float gravity = 9.81f * 4f;
    float jumpHeight = 2.5f;
    float velocityY = 0;
    //Animation stuff
    float fallTimer;
    float groundedTimer;
    public float knockedTimer;
    public float launchVelocity = 3f;
    public Vector3 launchDirection;
    //Player Bools
    public PlayerManager playerManager;

    void Awake() //called before start
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        playerManager = GetComponent<PlayerManager>();
    }

    void Start()
    {
        launchDirection = Vector3.zero;
    }

    private void FixedUpdate()
    {
        //For smooth animation (use GetAxis (fractional) instead of GetAxisRaw (integer))
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //For actual movement
        if (!playerManager.isCollecting && !playerManager.isKnocked && !playerManager.isInteracting)
        {
            HandleMovement();
        }
    }


    void Update()
    {
        CheckSprint();
        CheckCrouch();
        if (!playerManager.isInteracting)
        {
            HandleGravityAndJump();
        }
        HandleKnocked();
    }

    void HandleKnocked()
    {
        if (playerManager.isKnocked)
        {
            knockedTimer += Time.deltaTime;
            LaunchInDirection(launchDirection, launchVelocity);
            launchVelocity -= 1.5f*Time.deltaTime;
        }
        if(knockedTimer > 2)
        {
            playerManager.isKnocked = false;
            launchVelocity = 3f;
        }
    }

    void HandleMovement()
    {
        if (playerManager.isSprinting)
        {
            speed = sprintSpeed;
            controller.height = 2;
            controller.center = Vector3.up;
        }
        else if (playerManager.isCrouching)
        {
            speed = crouchSpeed;
            controller.height = 1;
            controller.center = new Vector3(0, 0.5f, 0);
        }
        else
        {
            speed = 3.5f;
            controller.height = 2;
            controller.center = Vector3.up;
        }

        //capturing Input from Player
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (movement.magnitude >= 0.1f) //if you move a little bit
        {
            playerManager.isMoving = true;
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y; //Gets the radian measure of target location, convert it to degrees, then consider the current camera angle
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            // gradually changes angle to a target angle over time 
            transform.rotation = Quaternion.Euler(0, currentAngle, 0); //apply that rotation with the angle (which is slowly rotating to target)
            Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward; // True direction movement from get axis raw
            controller.Move(rotatedMovement * speed * Time.fixedDeltaTime); //Doesn't move forward when angle is slowly changing, only moves the true direction of button pressed
        }
        if (movement == Vector3.zero)
        {
            playerManager.isMoving = false;
        }
    }

    void HandleGravityAndJump()
    {
        RaycastHit hit;
        bool frontRay = Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z + controller.radius), Vector3.down, out hit, 0.11f);
        bool backRay = Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z - controller.radius), Vector3.down, out hit, 0.11f);
        bool leftRay = Physics.Raycast(new Vector3(transform.position.x - controller.radius, transform.position.y + 0.1f, transform.position.z), Vector3.down, out hit, 0.11f);
        bool rightRay = Physics.Raycast(new Vector3(transform.position.x + controller.radius, transform.position.y + 0.1f, transform.position.z), Vector3.down, out hit, 0.11f);
        /*Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z + controller.radius), Vector3.down * 0.11f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z - controller.radius), Vector3.down * 0.11f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x - controller.radius, transform.position.y + 0.1f, transform.position.z), Vector3.down * 0.11f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x + controller.radius, transform.position.y + 0.1f, transform.position.z), Vector3.down * 0.11f, Color.red);
        Debug.DrawRay(transform.position, Vector3.down * 0.01f, Color.red);*/
        bool centerRay = Physics.Raycast(transform.position, Vector3.down, out hit, 0.01f);
        playerManager.isGrounded = frontRay || backRay || leftRay || rightRay || centerRay;

        /*
        if (isGrounded)
        {
            controller.stepOffset = 0f;
        }
        else
        {
            controller.stepOffset = 0;
        }*/

        if (playerManager.isGrounded)
        {
            groundedTimer = 0.2f;
            playerManager.isJumping = false;
            playerManager.isFalling = false;
            fallTimer = 0;
        }

        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }

        // slam into the ground
        if (playerManager.isGrounded && velocityY < 0)
        {
            // hit ground
            velocityY = 0f;
        }

        velocityY -= gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && !playerManager.isKnocked)
        {
            // must have been grounded recently to allow jump
            if (groundedTimer > 0)
            {
                // no more until we recontact ground
                groundedTimer = 0;

                // Physics dynamics formula for calculating jump up velocity based on height and gravity
                velocityY = 0;
                velocityY += Mathf.Sqrt(jumpHeight * 2 * gravity);

                playerManager.isJumping = true;
            }
        }

        if ((playerManager.isJumping && velocityY < 0) || velocityY < -2)
        {
            fallTimer += Time.deltaTime;
            if (fallTimer > 0.4)
            {
                playerManager.isFalling = true;
            }
        }

        controller.Move(new Vector3(0, velocityY, 0) * Time.deltaTime);
    }

    void CheckSprint()
    {
        if (playerManager.isCrouching || playerManager.isHolding)
        {
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerManager.isSprinting = true;
        }
        else
        {
            playerManager.isSprinting = false;
        }
    }

    void CheckCrouch()
    {
        if (playerManager.isSprinting || playerManager.isHolding)
        {
            return;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            playerManager.isCrouching = true;
        }
        else
        {
            playerManager.isCrouching = false;
        }
    }

    public void LaunchInDirection(Vector3 direction, float distance)
    {
        controller.Move(direction.normalized * distance * Time.deltaTime);
    }
}