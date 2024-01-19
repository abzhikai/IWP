using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject storyPanel;
    [SerializeField] Button storyButton;
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

    public void OpenStoryPanel()
    {
        storyPanel.SetActive(true);
    }

    public void CloseStoryPanel()
    {
        storyPanel.SetActive(false);
    }

}
