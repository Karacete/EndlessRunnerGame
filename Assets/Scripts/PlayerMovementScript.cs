using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    private float forwardSpeed;
    private int desiredLane;
    private float laneDistance;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        forwardSpeed = 0;
        desiredLane = 1;
        laneDistance = 4;
    }
    void Update()
    {
        direction.z = forwardSpeed;
        if(Input.GetKeyDown(KeyCode.D))
        {
            desiredLane += 1;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            desiredLane -= 1;
            if (desiredLane == -1)
                desiredLane = 0;
        }
        Vector3 targetPos = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPos += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPos += Vector3.right * laneDistance;
        transform.position = Vector3.Lerp(transform.position, targetPos, 10 * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        controller.Move(direction * Time.deltaTime);
    }
}
