using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChildObjectScript : MonoBehaviour
{
    [SerializeField]
    private GameObject losePanel;
    private int pointCount;
    [SerializeField]
    private TextMeshProUGUI pointText; 
    private void Start()
    {
        pointCount = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Barrier") || other.gameObject.CompareTag("Vehicle"))
        {
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            pointCount += 4;
            pointText.text = pointCount.ToString();
        }
    }
}
