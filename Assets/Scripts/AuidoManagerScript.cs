using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuidoManagerScript : MonoBehaviour
{
    private int sceneIndex;
    private void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex != 2)
            DontDestroyOnLoad(gameObject);
    }
}
