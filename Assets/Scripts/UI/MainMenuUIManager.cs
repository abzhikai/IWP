using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] Button startButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartGame()
    {
        ScenesManager.Instance.LoadScene("GameScene");
        Time.timeScale = 1;
    }
}
