using Unity.Mathematics;
using UnityEngine;

public class SkateMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float horizontalSpeed;
    private Animator skateboardAnim;
    private float minX = -4.5f;
    private float maxX = 4.5f;
    private UIManager uiManager;

    void Start()
    {
        speed = 20;
        horizontalSpeed = 5;
        skateboardAnim = GetComponent<Animator>();
        uiManager = GameObject.FindWithTag("UI").GetComponent<UIManager>();
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Left();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            StopLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Right();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            StopRight();
        }
        if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                float halfScreenWidth = Screen.width / 2;
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (touch.position.x > halfScreenWidth)
                        {
                            Right();
                        }
                        else if (touch.position.x < halfScreenWidth)
                        {
                            Left();
                        }
                        break;

                    case TouchPhase.Ended:

                    case TouchPhase.Canceled:
                        if (touch.position.x > halfScreenWidth)
                            StopRight();
                        else if (touch.position.x < halfScreenWidth)
                            StopLeft();
                        break;
                }
            }
    }
    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
        SpeedUp();
    }
    private void Left()
    {
        skateboardAnim.SetBool("isLeft", true);
        transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
    }
    private void StopLeft()
    {
        skateboardAnim.SetBool("isLeft", false);
    }
    private void Right()
    {
        skateboardAnim.SetBool("isRight", true);
        transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
    }
    private void StopRight()
    {
        skateboardAnim.SetBool("isRight", false);
    }
    private void SpeedUp()
    {
        if (uiManager.IsSkateboardSpeedup)
        {
            speed += 5;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Vehicle"))
        {
            Time.timeScale = 0;
        }
    }
}
