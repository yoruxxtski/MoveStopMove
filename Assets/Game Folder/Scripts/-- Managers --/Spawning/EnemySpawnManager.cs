using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : Singleton<EnemySpawnManager>
{
    //-------------------------------------- Attributes
    [Header("Enemies Elements")]
    [SerializeField] private int totalNumberOfEnemies; // * Tong so luong quai vat (ban dau = 99)
    private int numberOfHumanoidAlive; // * Tong so luong dang con song (phai + 1 vi nguoi cung tinh la alive) (ban dau = 100)
    [SerializeField] private int amountAliveAtOneTime; // * So luong quai vat dc spawn tai 1 thoi diem (tran san luc nao cung se chi co 10 enemies)
    private int numberOfEnemiesLeftToSpawn; // * So luong quai vat con lai co the spawn (so  luong quai vat co the spawn (ban dau = 99))
    // ------------------------------------
    [Header("Spawning Enemies Elements")]
    [SerializeField] private TagType enemyTag; 
    [SerializeField] private int spawnRange;
    [SerializeField] private float spawnCheckRadius; // * Check de spawn ben canh se ko co object 

    //--------------------------------------
    [Header("Objects Not To Spawn On")]
    [SerializeField] private List<GameObject> obstacleObject;

    // -------------------------------------
    [Header("Dealing with multiple enemies death at the same time")]
    private Queue<int> spawnQueue = new Queue<int>(); // Queue to track enemies to spawn
    private bool isSpawning = false; // Flag to prevent multiple spawn calls

    //----------------------------------------- UNITY FUNCTIONS
    void Start()
    {
        numberOfHumanoidAlive = totalNumberOfEnemies + 1; 
        numberOfEnemiesLeftToSpawn = totalNumberOfEnemies;
        amountAliveAtOneTime = Math.Min(totalNumberOfEnemies, amountAliveAtOneTime);

        for (int i = 0; i < amountAliveAtOneTime; i++) {
            SpawnEnemies();
        }

        StartCoroutine(SpawnCheckRoutine());
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnCheckRadius);
    }

    // ----------------------------------------- USER DEFINED FUNCTIONS

    public Vector3 GetRandomLocation() {
        Vector3 spawnPosition = Vector3.zero;
        bool foundValidSpawn = false;

        // Try up to 100 times to find a valid spawn position
        for (int attempt = 0; attempt < 100; attempt++)
        {
            float posX = UnityEngine.Random.Range(-spawnRange, spawnRange);
            float posY = UnityEngine.Random.Range(-spawnRange, spawnRange);
            spawnPosition = new Vector3(posX, 0, posY);

            // Check if this position is far enough from obstacles
            bool farEnough = true;
            foreach (GameObject obstacle in obstacleObject)
            {
                if (Vector3.Distance(spawnPosition, obstacle.transform.position) < spawnCheckRadius)
                {
                    farEnough = false;
                    break; // No need to check further obstacles if one is too close
                }
            }

            if (farEnough)
            {
                foundValidSpawn = true;
                break; // Found a valid spawn position, exit the loop
            }
        }

        // If after 100 attempts no valid spawn position was found, use the last attempt position
        if (!foundValidSpawn)
        {
            Debug.LogWarning("Could not find a valid spawn position after 100 attempts. Using the last attempt position.");
        }

        return spawnPosition;
    }

    public void SpawnEnemies()
    {
        if (numberOfEnemiesLeftToSpawn > 0)
        {
            Vector3 spawnPos = GetRandomLocation();
            GameObject enemy = PoolingManager.instance.SpawnFromPool(enemyTag, spawnPos, Quaternion.identity);

            if (enemy != null)
            {
                enemy.layer = LayerMask.NameToLayer("Enemy");
                numberOfEnemiesLeftToSpawn--;
                Debug.Log("Spawned enemy. Remaining: " + numberOfEnemiesLeftToSpawn);
            }
            else
            {
                Debug.LogWarning("Failed to spawn enemy. Pool might be empty.");
            }
        }
        else
        {
            Debug.LogWarning("No more enemies left to spawn.");
        }
    }

    public void DeactivateEnemy(GameObject gameObject)
    {
        PoolingManager.instance.DeActiveEnemyToPool(gameObject);
    }

    private IEnumerator SpawnCheckRoutine()
    {
        while (true)
        {
            // Check if there are no enemies left to spawn
            if (numberOfEnemiesLeftToSpawn <= 0)
            {
                Debug.Log("No more enemies left to spawn. Stopping SpawnCheckRoutine.");
                yield break; // Stop the coroutine
            }
            int currentAliveEnemies = GameObject.FindGameObjectsWithTag(enemyTag.ToString()).Length; // return active array with tags
            if (currentAliveEnemies < amountAliveAtOneTime)
            {
                int enemiesToSpawn = amountAliveAtOneTime - currentAliveEnemies;
                Debug.Log("SpawnCheckRoutine: Current alive enemies: " + currentAliveEnemies + ", Enemies to spawn: " + enemiesToSpawn);
                for (int i = 0; i < enemiesToSpawn && numberOfEnemiesLeftToSpawn > 0; i++)
                {
                    SpawnEnemies();
                }
            }
            yield return new WaitForSeconds(1.0f); // Check every second
        }
    }
    
    // -------------------------------------- Getter & Setter
    public TagType GetEnemyTagType()
    {
        return enemyTag;
    }

}