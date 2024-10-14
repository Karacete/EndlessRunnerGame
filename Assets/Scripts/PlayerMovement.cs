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
    private bool isRolling;
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
    [SerializeField]
    private GameObject childObject;
    private Animator animator;
    private AnimatorClipInfo[] animatorClips;
    private CapsuleCollider capsuleCol;
    void Start()
    {
        speed = 11.5f;
        rb = GetComponent<Rigidbody>();
        desiredLine = 1;
        laneDistance = 3.4f;
        jumpForce = 6.3f;
        isGrounded = true;
        isRolling = false;
        gravity = 3f;
        newDesired = desiredLine;
        oldDesired = 4;
        chance = 2;
        animator = GetComponent<Animator>();
        capsuleCol = this.gameObject.GetComponent<CapsuleCollider>();
    }
    private void FixedUpdate()
    {
        if (mainCam.activeInHierarchy)
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
        if (!isRolling)
            return;
        childObject.SetActive(false);
        capsuleCol.height = .4f;
        capsuleCol.center = new Vector3(0, .3f, 0);
        animatorClips = this.animator.GetCurrentAnimatorClipInfo(0);
        if (animatorClips[0].clip.name == "Stand To Roll")
        {
            animator.SetBool("IsRolling", false);
            isRolling = false;
        }
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.deltaPosition.x > 30)
            {
                desiredLine += 1;
                if (desiredLine > 2)
                    desiredLine = 2;
                DesiredChanged();
            }
            if (touch.deltaPosition.x < -30)
            {
                desiredLine -= 1;
                if (desiredLine == -1)
                    desiredLine = 0;
                DesiredChanged();
            }
            if (touch.deltaPosition.y > 20)
            {
                if (isGrounded)
                    Jump();
            }
            if (touch.deltaPosition.y < -20)
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
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            if (chance == 1)
            {
                chance = 2;
                redLight.SetActive(false);
                blueLight.SetActive(false);
                StopAllCoroutines();
            }
            childObject.SetActive(true);
            capsuleCol.height = 1.4f;
            capsuleCol.center = new Vector3(0, .7f, 0);
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
        rb.velocity = new Vector3(0, 0, speed);
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