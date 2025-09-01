using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform targetPos;
    void Start()
    {
        targetPos = GameObject.FindWithTag("Skateboard").transform;
    }
    void Update()
    {
        Vector3 newPos = new Vector3(targetPos.position.x, targetPos.position.y, targetPos.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, .3f);
    }
}