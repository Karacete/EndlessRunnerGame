using UnityEngine;

public class ChildObjectScript : MonoBehaviour
{
    [SerializeField]
    private GameObject losePanel;
    private BoxCollider boxCol;
    private PlayerMovement player;
    private void Start()
    {
        boxCol = this.gameObject.GetComponent<BoxCollider>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Barrier") || other.gameObject.CompareTag("Vehicle"))
        {
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }
    }
    private void Update()
    {
        if (player.isRollingPublic)
        {
            boxCol.size = new Vector3(1, .2f, 1);
            boxCol.center = new Vector3(0, -.4f, 0);
        }
        if (!player.isRollingPublic)
        {
            boxCol.size = new Vector3(1, 1.5f, 1);
            boxCol.center = new Vector3(0, 0, 0);
        }
    }
}
