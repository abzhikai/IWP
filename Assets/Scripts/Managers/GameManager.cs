using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float mapYBoundaries;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (player.transform.position.y < mapYBoundaries)
        {
            player.transform.position = playerSpawnPoint.transform.position;
        }
    }
}
