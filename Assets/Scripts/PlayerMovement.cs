using System;
using System.Collections;
using System.Drawing;
using TMPro;
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
    private bool isRolling;
    private float gravity;
    private int oldDesired;
    private int newDesired;
    [SerializeField]
    private GameObject mainCam;
    private int chance;
    [SerializeField]
    private GameObject losePanel;
    private Animator animator;
    private AnimatorClipInfo[] animatorClips;
    private CapsuleCollider capsuleCol;
    [SerializeField]
    private TextMeshProUGUI pointText;
    private double point;
    public bool isRollingPublic
    {
        get
        {
            return isRolling;
        }
        set
        {
            isRolling = value;
        }
    }
    void Start()
    {
        speed = 10f;
        rb = GetComponent<Rigidbody>();
        desiredLine = 1;
        laneDistance = 3.6f;
        jumpForce = 6f;
        isGrounded = true;
        isRolling = false;
        gravity = 3f;
        newDesired = desiredLine;
        oldDesired = 4;
        chance = 2;
        animator = GetComponent<Animator>();
        capsuleCol = gameObject.GetComponent<CapsuleCollider>();
    }
    private void FixedUpdate()
    {
        point += .1f;
        point = Math.Round(point, 2);
        pointText.text = point.ToString();
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontal * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
        if (!isGrounded)
            rb.AddForce(0, -gravity, 0);
        if (chance == 0)
        {
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }
        if (isRolling)
        {
            capsuleCol.height = .4f;
            capsuleCol.center = new Vector3(0, .3f, 0);
            animatorClips = animator.GetCurrentAnimatorClipInfo(0);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rolling") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                animator.SetBool("IsRolling", false);
                isRolling = false;
            }
            if (!isRolling)
            {
                capsuleCol.height = 1.4f;
                capsuleCol.center = new Vector3(0, .7f, 0);
            }
        }
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.deltaPosition.x > 100)
            {
                desiredLine += 1;
                if (desiredLine > 2)
                    desiredLine = 2;
                DesiredChanged();
            }
            if (touch.deltaPosition.x < -100)
            {
                desiredLine -= 1;
                if (desiredLine == -1)
                    desiredLine = 0;
                DesiredChanged();
            }
            if (touch.deltaPosition.y > 80)
            {
                if (isGrounded)
                    Jump();
            }
            if (touch.deltaPosition.y < -80)
            {
                Roll();
            }
        }
        horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.D))
        {
            desiredLine += 1;
            if (desiredLine > 2)
                desiredLine = 2;
            DesiredChanged();

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            desiredLine -= 1;
            if (desiredLine == -1)
                desiredLine = 0;
            DesiredChanged();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded)
                Jump();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Roll();
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
        {
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
        if (collision.gameObject.CompareTag("Vehicle"))
        {
            desiredLine = oldDesired;
            newDesired = desiredLine;
            oldDesired = newDesired;
            chance -= 1;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Coin") && isRolling)
        {
            if (chance == 1)
            {
                chance = 2;
            }
        }
    }
    private void DesiredChanged()
    {
        oldDesired = newDesired;
        newDesired = desiredLine;
    }
    private void Jump()
    {
        rb.linearVelocity = new Vector3(0, jumpForce, 0);
        animator.SetBool("IsJumping", true);
        animator.SetBool("IsGrounded", false);
        isGrounded = false;
        animator.SetBool("IsRolling", false);
        isRolling = false;
    }
    private void Roll()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsRolling", true);
        isRolling = true;
        rb.linearVelocity = new Vector3(0, 0, speed);
    }
}