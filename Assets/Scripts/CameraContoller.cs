using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    private Transform targetPos;
    private Vector3 offset;
    void Start()
    {
        targetPos = GameObject.FindWithTag("Player").transform;
        offset = this.gameObject.transform.position - targetPos.position;
    }
    void Update()
    {
        Vector3 newPos = new Vector3(targetPos.position.x, transform.position.y, offset.z + targetPos.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, 100);
    }
}
