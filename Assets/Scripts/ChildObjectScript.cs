using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildObjectScript : MonoBehaviour
{
    [SerializeField]
    private GameObject losePanel;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hata nerede");
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Debug.Log("carpti");
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }
    }
}
