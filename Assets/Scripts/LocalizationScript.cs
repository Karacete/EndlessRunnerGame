using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationScript : MonoBehaviour
{
    private bool active;
    void Start()
    {
        active = false;
        
    }
    public void LanguageChange(int id)
    {
        if (active)
            return;
        StartCoroutine(SetLocale(id));
        PlayerPrefs.SetInt("Language", id);
        PlayerPrefs.Save();
    }
    protected IEnumerator SetLocale(int number)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[number];
        active = false;
    }
}
