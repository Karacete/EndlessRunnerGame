using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private Rigidbody rb;
    private float horizontal;
    private int desiredLine;
    private float laneDistance;
    private float jumpForce;
    private bool isGrounded;
    private float gravity;
    private GameObject childObject;
    void Start()
    {
        speed = 10f;
        rb = GetComponent<Rigidbody>();
        desiredLine = 1;
        laneDistance = 3.4f;
        jumpForce = 7f;
        isGrounded = true;
        gravity = 3f;
        childObject = this.gameObject.transform.GetChild(0).gameObject;
    }
    private void FixedUpdate()
    {
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontal * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
        if (!isGrounded)
            rb.AddForce(0, -gravity, 0);
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.D))
        {
            desiredLine += 1;
            if (desiredLine > 2)
                desiredLine = 2;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            desiredLine -= 1;
            if (desiredLine == -1)
                desiredLine = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
                Jump();
        }
        Vector3 targetPos = rb.position.z * transform.forward + rb.position.y * transform.up;
        if (desiredLine == 0)
            targetPos += Vector3.left * laneDistance;
        else if (desiredLine == 2)
            targetPos += Vector3.right * laneDistance;
        rb.position = Vector3.Lerp(rb.position, targetPos, 10 * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Debug.Log("bu çarptý");
            desiredLine = 1;
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(0,jumpForce,0);
        isGrounded = false;
    }
}
