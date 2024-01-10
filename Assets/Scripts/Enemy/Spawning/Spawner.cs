using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject spawningArea;
    [SerializeField] GameObject spawningPosition;
    [SerializeField] GameObject enemyPrefab;
    GameObject enemyList;

    int enemycount;
    bool validPosByLine;
    bool spawn;
    int spawnAmt;
    Coroutine corSpawn;

    [SerializeField] int maxSpawnCount = 3;

    [SerializeField] float spawnCooldown = 30;
    float spawnTimer = 30;

    // Start is called before the first frame update
    void Start()
    {
        enemycount = 0;
        spawn = false;
        enemyList = GameObject.Find("EnemyList");
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn groups of enemies if enemy is inside of the spawning range
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnCooldown && spawningArea.GetComponent<SpawnZone>().GetStartSpawn() == true)
        {
            SpawnGroup();
            spawnTimer = 0;
        }

        //enemycount = enemyList.transform.childCount;
        // If there are no enemies, instantiate them after a period of time
        //if (enemycount == 0)
        //{
        //    // Check if the area is somewhere feasible
        //    validPosCheck();
        //    // Spawn the prefab
        //    if (spawn)
        //    {
        //        randomSpawn();
        //        spawn = false;
        //    }
        //    SpawnGroup();
        //}
    }

    Vector2 GetRandomPos()
    {
        //float minX = spawningArea.transform.position.x - spawningArea.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        //float maxX = spawningArea.transform.position.x + spawningArea.GetComponent<BoxCollider2D>().bounds.size.x / 2;

        //float minY = spawningArea.transform.position.y - spawningArea.GetComponent<BoxCollider2D>().bounds.size.y / 2;
        //float maxY = spawningArea.transform.position.y + spawningArea.GetComponent<BoxCollider2D>().bounds.size.y / 2;

        //float randomX = Random.Range(minX, maxX);
        //float randomY = Random.Range(minY, maxY);

        //return new Vector2(randomX, randomY);

        float randomX = Random.Range(spawningArea.GetComponent<BoxCollider2D>().bounds.min.x, spawningArea.GetComponent<BoxCollider2D>().bounds.max.x);
        float randomY = Random.Range(spawningArea.GetComponent<BoxCollider2D>().bounds.min.y, spawningArea.GetComponent<BoxCollider2D>().bounds.max.y);

        // Create a new Vector2 with the random coordinates
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        return spawnPosition;
    }

    void randomSpawn()
    {
        spawnAmt = Random.Range(1, maxSpawnCount + 1);
        for (int i = 0; i < spawnAmt; i++)
        {
            // Get a random position within the spawning area
            spawningPosition.transform.position = GetRandomPos();
            // Set the enemy to spawn in the enemy list
            GameObject enemy = Instantiate(enemyPrefab, spawningPosition.transform.position, Quaternion.identity);
            enemy.transform.parent = enemyList.transform;
            Debug.Log("Enemy Spawned");
        }
        spawn = false;
    }

    void validPosCheck()
    {
        Collider2D collider = Physics2D.OverlapCircle((Vector2)transform.position, 10, LayerMask.GetMask("Default"));
        Vector3 spawningPositionCheckEndPoint = spawningPosition.transform.localPosition - new Vector3(0, 3, 0);
        validPosByLine = Physics2D.Linecast(spawningPosition.transform.localPosition, spawningPositionCheckEndPoint, ~LayerMask.GetMask("Terrain"));

        if (collider == null)
        {
            if (validPosByLine)
            {
                spawn = true;
            }
            else
            {
                spawn = false;
            }
        }
    }

    void SpawnGroup()
    {
        spawnAmt = Random.Range(1, maxSpawnCount + 1);
        if (corSpawn == null)
        {
            corSpawn = StartCoroutine(SpawnCheck(spawnAmt));
        }
    }

    IEnumerator SpawnCheck(int amount)
    {
        int counter = amount;
        bool loop = true;
        List<Vector3> vector3List = new List<Vector3>();
        while (loop)
        {
            spawningPosition.transform.position = GetRandomPos();
            Collider2D collider = Physics2D.OverlapCircle(spawningPosition.transform.position, 3, LayerMask.GetMask("Default"));
            RaycastHit2D hit = Physics2D.Raycast(spawningPosition.transform.position, Vector2.down, Mathf.Infinity, ~LayerMask.GetMask("Zone"));
            if (collider == null && hit.collider != null)
            {
                counter--;
                // Set the spawn position to be close to the ground
                spawningPosition.transform.position = new Vector3(spawningPosition.transform.position.x, hit.point.y + 3, spawningPosition.transform.position.z);
                vector3List.Add(spawningPosition.transform.position);
                if (counter == 0)
                {
                    loop = false;
                }
            }
            yield return null;
        }
        // Set the enemy to spawn in the enemy list
        foreach (var r in vector3List)
        {
            GameObject enemy = Instantiate(enemyPrefab, r, Quaternion.identity);
            enemy.transform.parent = enemyList.transform;
        }
        corSpawn = null;
    }
}