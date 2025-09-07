using System;
using TMPro;
using UnityEditor.Localization.Platform.Android;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    private int counter = 0;
    [SerializeField] private TextMeshProUGUI counterText;
    private bool isSkateboardSpeedup = false;
    public bool IsSkateboardSpeedup
    {
        get { return isSkateboardSpeedup; }
        set { isSkateboardSpeedup = value; }
    }
    private void SkateboardSpeedup()
    {
        if (counter % 1000 == 0 && counter != 0)
        {
            isSkateboardSpeedup = true;
        }
        else
            isSkateboardSpeedup = false;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    private void FixedUpdate()
    {
        counter++;
        counterText.text = counter.ToString("D6");
        SkateboardSpeedup();
    }
}