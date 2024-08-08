using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildObjectScript : MonoBehaviour
{
    [SerializeField]
    private GameObject losePanel;
        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Barrier"))
        {
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }
    }
}
