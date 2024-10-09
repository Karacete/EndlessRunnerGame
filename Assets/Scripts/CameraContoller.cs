using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    private Transform targetPos;
    private Vector3 offset;
    private Animator animator;
    void Start()
    {
        targetPos = GameObject.FindWithTag("Player").transform;
        offset = this.gameObject.transform.position - targetPos.position;
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        animator.SetFloat("Speed", 0.5f);
    }
    void Update()
    {
        Vector3 newPos = new Vector3(targetPos.position.x, transform.position.y, offset.z + targetPos.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, 100);
    }
}
