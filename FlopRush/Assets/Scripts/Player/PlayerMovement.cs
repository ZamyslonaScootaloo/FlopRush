using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jump = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    CapsuleCollider playerCol;

    float originalHeight;
    public float reduceHeight;

    Vector3 velocity;
    bool isGrounded;
    
    void Start()
    {
        player.GetComponent<CharacterController>();
        playerCol = GetComponent<CapsuleCollider>();
        originalHeight = player.GetComponent<CharacterController>().height;
    }

    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move*speed*Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump*-2f*gravity);
        }
        if (Input.GetKeyDown(KeyCode.C))
            Crouch();
        else if (Input.GetKeyUp(KeyCode.C))
            GoUp();
        

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void Crouch()
    {
        player.GetComponent<CharacterController>().height = reduceHeight;
    }
    void GoUp()
    {
        player.GetComponent<CharacterController>().height = originalHeight;
    }
}
