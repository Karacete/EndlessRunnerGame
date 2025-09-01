using UnityEngine;

public class SkateMovement : MonoBehaviour
{
    private float speed;
    private float horizontalSpeed;
    private Animator skateboardAnim;
    private float minX = -4.5f;
    private float maxX = 4.5f;

    void Start()
    {
        speed = 20;
        horizontalSpeed = 5;
        skateboardAnim = GetComponent<Animator>();
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
        {
            skateboardAnim.SetBool("isLeft", true);
            transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            skateboardAnim.SetBool("isLeft", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            skateboardAnim.SetBool("isRight", true);
            transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            skateboardAnim.SetBool("isRight", false);
        }
    }
    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Vehicle"))
        {
            Time.timeScale = 0;
        }
    }
}
