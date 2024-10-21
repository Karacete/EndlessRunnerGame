using System;
using TMPro;
using UnityEngine;

public class ChildObjectScript : MonoBehaviour
{
    [SerializeField]
    private GameObject losePanel;
    private BoxCollider boxCol;
    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();
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
