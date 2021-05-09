using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public Transform GroundCheck;
    public LayerMask GroundMask;
    private float GroundDistance = 0.4f;
    private float Speed = 12f;
    private float JumpHeight = 5f;
    private float Gravity = -9.81f * 2;
    private Vector3 velocity;
    private bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float speedBoost = 0f;

        if (Input.GetKey(KeyCode.LeftShift))
            speedBoost = Speed * 0.7f;

        Vector3 move = transform.right * x + transform.forward * z;

        Controller.Move(move * (Speed + speedBoost) * Time.deltaTime);

        //if (Input.GetButtonDown("Jump") && isGrounded)
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt((JumpHeight + (speedBoost * 0.5f)) * -2 * Gravity);
        }

        velocity.y += Gravity * Time.deltaTime;

        Controller.Move(velocity * Time.deltaTime);
    }
}
