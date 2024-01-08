using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] GameObject playerSpawnPoint;
    [SerializeField] GameObject gameUIManager;
    public void RespawnPlayer()
    {
        GetComponent<PlayerStats>().Respawn();
        transform.position = playerSpawnPoint.transform.position;
    }
}
