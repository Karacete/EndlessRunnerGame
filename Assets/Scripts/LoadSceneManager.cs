using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField]
    private Image loadingImage;
    [SerializeField]
    private GameObject loadingPanel;
    public void Load(int level)
    {
        loadingPanel.SetActive(true);
        StartCoroutine(StartLoading(level));
    }
    protected IEnumerator StartLoading(int level)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
        while(!operation.isDone)
        {
            loadingImage.fillAmount = operation.progress;
            yield return null;
        }
    }
}
