using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform targetPos;
    private UIManager uiManager;
    void Start()
    {
        targetPos = GameObject.FindWithTag("Skateboard").transform;
        uiManager = GameObject.FindWithTag("UI").GetComponent<UIManager>();
    }
    void Update()
    {
        Vector3 newPos = new Vector3(targetPos.position.x, targetPos.position.y, targetPos.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, .5f);
    }
    void FixedUpdate()
    {
        SpeedUpAnimation();
    }
    private void SpeedUpAnimation()
    {
        if (uiManager.IsSkateboardSpeedup)
        {
            GetComponent<Animator>().SetBool("isSpeedUp", true);
            Debug.Log("Speed Up!");
        }
        else
            GetComponent<Animator>().SetBool("isSpeedUp", false);
    }
}