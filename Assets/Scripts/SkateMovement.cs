using UnityEngine;

public class SkateMovement : MonoBehaviour
{
    private float speed;
    private float horizontalSpeed;
    private Animator skateboardAnim;
    void Start()
    {
        speed = 10;
        horizontalSpeed = 4;
        skateboardAnim = GetComponent<Animator>();
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            skateboardAnim.SetBool("isLeft", true);
            transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            skateboardAnim.SetBool("isLeft", false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            skateboardAnim.SetBool("isRight", true);
            transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            skateboardAnim.SetBool("isRight", false);
        }
    }
}
