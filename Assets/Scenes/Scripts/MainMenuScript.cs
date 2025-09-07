using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private int curLang;
    private LoadSceneManager loadScene;
    private LocalizationScript localization;
    private void Start()
    {
        loadScene = GameObject.FindWithTag("LoadScene").GetComponent<LoadSceneManager>();
        localization = GameObject.FindWithTag("Localization").GetComponent<LocalizationScript>();
        if (PlayerPrefs.HasKey("Language"))
            PlayerPrefs.GetInt("Language");
        else
            PlayerPrefs.SetInt("Language", 0);

        if (PlayerPrefs.HasKey("Volume"))
            PlayerPrefs.GetInt("Volume");
        else
            PlayerPrefs.SetInt("Volume", 1);

        curLang = PlayerPrefs.GetInt("Language");
        AudioListener.volume = PlayerPrefs.GetInt("Volume");
        localization.LanguageChange(curLang);
    }
    public void Play()
    {
        loadScene.Load(2);
    }
    public void Settings()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
