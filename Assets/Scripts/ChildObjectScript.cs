using TMPro;
using UnityEngine;

public class ChildObjectScript : MonoBehaviour
{
    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private TextMeshProUGUI pointText;
    private void Start()
    {

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
