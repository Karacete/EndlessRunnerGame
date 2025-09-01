using System;
using TMPro;
using UnityEditor.Localization.Platform.Android;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    private int point;
    private int chance = 3; // Assuming player starts with 3 chances
    public static UIManager Instance { get; private set; }
    private int counter = 0;
    [SerializeField] private TextMeshProUGUI counterText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoint(int points)
    {
        point += points;
    }

    public void LoseChance()
    {
        if (chance > 0)
        {
            chance--;
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    private void FixedUpdate()
    {
        counter++;
        counterText.text = counter.ToString("D6");
    }
}