using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{
    [SerializeField]
    private GameObject voiceOnButton;
    [SerializeField]
    private GameObject voiceOffButton;
    [SerializeField]
    private GameObject english;
    [SerializeField]
    private GameObject turkish;
    void Start()
    {
        switch (PlayerPrefs.GetInt("Language"))
        {
            case 0:
                LanguageChange(false);
                break;
            case 1:
                LanguageChange(true);
                break;
        }

        switch (PlayerPrefs.GetInt("Volume"))
        {
            case 0:
                VoiceChange(false);
                break;
            case 1:
                VoiceChange(true);
                break;
        }
    }
    public void VoiceChange(bool isSound)
    {
        if (isSound)
        {
            voiceOffButton.SetActive(false);
            voiceOnButton.SetActive(true);
            AudioListener.volume = 1;
        }
        else
        {
            voiceOnButton.SetActive(false);
            voiceOffButton.SetActive(true);
            AudioListener.volume = 0;
        }
        PlayerPrefs.SetInt("Volume", Convert.ToInt32(AudioListener.volume));
        PlayerPrefs.Save();
    }
    public void LanguageChange(bool isTurkish)
    {
        if (isTurkish)
        {
            english.SetActive(false);
            turkish.SetActive(true);
        }
        else
        {
            turkish.SetActive(false);
            english.SetActive(true);
        }
        PlayerPrefs.SetInt("Language", Convert.ToInt32(isTurkish));
        PlayerPrefs.Save();
    }
    public void Return()
    {
        SceneManager.LoadSceneAsync(0);
    }
}

