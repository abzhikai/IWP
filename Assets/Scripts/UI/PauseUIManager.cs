using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIManager : MonoBehaviour
{
    [SerializeField] Button pauseButton;
    [SerializeField] GameObject pausePanel;
    [SerializeField] Button continueButton;
    [SerializeField] Button mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        pauseButton.onClick.AddListener(OpenPanel);
        continueButton.onClick.AddListener(ReturnToGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf)
            {
                ReturnToGame();
            }
            else
            {
                OpenPanel();
            }
        }
    }

    public void OpenPanel()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReturnToGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        ScenesManager.Instance.LoadScene("MainMenu");
    }
}
