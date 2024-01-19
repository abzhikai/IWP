using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Map Respawn
    [SerializeField] float mapYBoundaries;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerSpawnPoint;

    // Player Objectives
    [SerializeField] TextMeshProUGUI notices;

    [SerializeField] float noticeTimeout = 5;
    float timer = 0;
    bool noticeUpdated = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(noticeUpdated && timer >= noticeTimeout)
        {
            notices.text = "";
            noticeUpdated = false;
            timer = 0;
        }
    }

    void FixedUpdate()
    {
        if (player.transform.position.y < mapYBoundaries)
        {
            player.transform.position = playerSpawnPoint.transform.position;
        }
    }

    public void NoticeUpdate(string notice)
    {
        notices.text = notice;
        noticeUpdated = true;
    }
}
