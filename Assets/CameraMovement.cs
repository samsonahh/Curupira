using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; //Hold the info for the player's pos
    public float mouseSensitivity; //Different for every pc
    public float xRot; //Stands for x rotation
    public float minY, maxY; //So you dont flip your camera all the way up or down.

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //sets player variable to the actual player in the scene
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position; //Set camera's position to player's position
        MouseLook(); //Use the method MouseLook()
    }

    void MouseLook()
    {
        //Use GetAxis here NOT GetAxisRaw
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY; //Think of stick going through sides of your body. Looking up rotates the x axis up and looking down rotates x axis down.
        xRot = Mathf.Clamp(xRot, minY, maxY); // Prevents xRot from going past min and max

        transform.localRotation = Quaternion.Euler(xRot, 0, 0); //Camera rotates based on y position of mouse.
        //Quaternion represents rotation. Does rotation maths for you.
        //Euler() returns the number in an order of z,x,y. Where y has the least priority.

        player.Rotate(Vector3.up * mouseX); //Rotates player based on x position of mouse.
    }
}
