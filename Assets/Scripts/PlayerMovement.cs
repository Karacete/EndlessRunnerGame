using System;
using System.Collections;
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
    private bool isJumping;
    private float gravity;
    private int oldDesired;
    private int newDesired;
    [SerializeField]
    private GameObject mainCam;
    [SerializeField]
    private GameObject redLight;
    [SerializeField]
    private GameObject blueLight;
    private int chance;
    [SerializeField]
    private GameObject losePanel;
    private Animator animator;
    void Start()
    {
        speed = 11.5f;
        rb = GetComponent<Rigidbody>();
        desiredLine = 1;
        laneDistance = 3.4f;
        jumpForce = 7f;
        isGrounded = true;
        gravity = 3f;
        newDesired = desiredLine;
        oldDesired = 4;
        chance = 2;
        animator = GetComponent<Animator>();
    }

    [Obsolete]
    private void FixedUpdate()
    {
        if (mainCam.active)
        {
            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
            Vector3 horizontalMove = transform.right * horizontal * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMove + horizontalMove);
            if (!isGrounded)
                rb.AddForce(0, -gravity, 0);
        }
        if (chance == 1)
            StartCoroutine(LightManager());
        if (chance == 0)
        {
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }
        Debug.Log(chance);
    }
    void Update()
    {
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
        {
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            //animator.SetBool("IsFalling", false);
        }
        if (collision.gameObject.CompareTag("Vehicle"))
        {
            desiredLine = oldDesired;
            newDesired = desiredLine;
            oldDesired = newDesired;
            chance -= 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Coin") && chance == 1)
        {
            chance = 2;
            redLight.SetActive(false);
            blueLight.SetActive(false);
            StopAllCoroutines();
        }
    }
    private void DesiredChanged()
    {
        oldDesired = newDesired;
        newDesired = desiredLine;
    }
    private void Jump()
    {
        rb.velocity = new Vector3(0, jumpForce, 0);
        animator.SetBool("IsJumping", true);
        isJumping = true;
        animator.SetBool("IsGrounded", false);
        isGrounded = false;
        if (isJumping && this.gameObject.transform.position.y > 1.5)
        {
            //animator.SetBool("IsFalling", true);
            animator.SetBool("IsJumping", false);
        }
    }
    private IEnumerator LightManager()
    {

        blueLight.SetActive(true);
        redLight.SetActive(false);
        yield return new WaitForSeconds(.5f);
        blueLight.SetActive(false);
        redLight.SetActive(true);
    }
}
