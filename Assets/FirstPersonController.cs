using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public Vector3 moveDirection; //Holds the 3d position of our character
    public float speed = 5.0f; //speed
    public bool isGrounded;
    public Rigidbody rb;
    public GameObject cam;
    public float jumpForce = 150f;
    public Animator anim;
    public bool isCollecting = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.Find("MainCamera");
        anim = GameObject.Find("SkeleModel").GetComponent<Animator>();
    }

    // // Fixed update only runs when the physics aspect of our game is active. More effiecient than Update() in this case.
    void FixedUpdate()
    {
        if (!isCollecting)
        {
            float x = Input.GetAxis("Horizontal"); //A, D, Left, and Right
            float z = Input.GetAxis("Vertical"); //W, S, Up, and Down

            moveDirection = new Vector3(x, 0, z); // y = 0 in this case because we are not working with jumping
            transform.position = (transform.position + moveDirection * Time.fixedDeltaTime * speed); // puts everything together to get WASD movement working
            //transform.Translate(moveDirection * Time.fixedDeltaTime * speed, Space.World); // puts everything together to get WASD movement working

            if(moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360 * Time.fixedDeltaTime);
            }
        }
    }

    void Update()
    {
        //transform.rotation = Quaternion.Euler(new Vector3(0, cam.transform.rotation.eulerAngles.y, 0));

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }

        anim.SetFloat("MoveX", moveDirection.x);
        anim.SetFloat("MoveZ", moveDirection.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}