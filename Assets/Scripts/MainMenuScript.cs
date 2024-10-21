using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private LoadSceneManager loadScene;
    private void Start()
    {
        loadScene = GameObject.FindWithTag("LoadScene").GetComponent<LoadSceneManager>();
        if (PlayerPrefs.HasKey("Language"))
            PlayerPrefs.GetInt("Language");
        else
            PlayerPrefs.SetInt("Language", 0);

        if (PlayerPrefs.HasKey("Volume"))
            PlayerPrefs.GetInt("Volume");
        else
            PlayerPrefs.SetInt("Volume", 1);

        AudioListener.volume = PlayerPrefs.GetInt("Volume");
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
