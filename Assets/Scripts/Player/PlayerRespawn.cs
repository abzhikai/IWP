using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] GameObject playerSpawnPoint;
    [SerializeField] GameObject gameUIManager;
    public void RespawnPlayer()
    {
        GetComponent<PlayerStats>().Respawn();
        transform.position = playerSpawnPoint.transform.position;
        SceneManager.LoadScene("GameScene");
    }
}
