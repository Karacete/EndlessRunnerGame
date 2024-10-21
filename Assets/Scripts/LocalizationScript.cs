using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationScript : MonoBehaviour
{
    private int language;
    private bool active;
    void Start()
    {
        language = PlayerPrefs.GetInt("Language");
        active = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LanguageChange(int id)
    {
        if (active)
            return;
        StartCoroutine(SetLocale(id));
        PlayerPrefs.SetInt("language", id);
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
