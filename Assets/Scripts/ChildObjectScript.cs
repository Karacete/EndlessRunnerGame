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
}
