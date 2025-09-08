using UnityEngine;

public class Items : MonoBehaviour
{
    private int coinCount = 0;
    private UIManager uiManager;
    void Start()
    {
        uiManager = GameObject.FindWithTag("UI").GetComponent<UIManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            uiManager.CoinTextUpdate(++coinCount);
        }
    }
}
