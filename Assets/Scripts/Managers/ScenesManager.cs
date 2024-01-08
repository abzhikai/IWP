using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            ScenesManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void ChangeSceneOnClick(string sceneName)
    {
        FindObjectOfType<ScenesManager>().LoadScene(sceneName);
    }
}
