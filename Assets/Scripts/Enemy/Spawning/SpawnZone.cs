using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    bool startSpawn;

    void Start()
    {
        startSpawn = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            startSpawn = true;
        }
        else
        {
            startSpawn = false;
        }
    }

    public bool GetStartSpawn()
    {
        return startSpawn;
    }
}
