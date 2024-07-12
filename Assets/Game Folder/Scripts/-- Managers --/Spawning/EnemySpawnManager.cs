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

    //--------------------------------------
    private List<GameObject> activeEnemies = new List<GameObject>(); // Track active enemies
    private int enemiesToDeactivate = 0; // Track number of enemies being deactivated

    //----------------------------------------- UNITY FUNCTIONS
    void Start()
    {
        numberOfHumanoidAlive = totalNumberOfEnemies + 1; 
        numberOfEnemiesLeftToSpawn = totalNumberOfEnemies;
        amountAliveAtOneTime = Math.Min(totalNumberOfEnemies, amountAliveAtOneTime);

        for (int i = 0; i < amountAliveAtOneTime; i++) {
            SpawnEnemies();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnCheckRadius);
    }

    public Vector3 GetRandomLocation() {
        Vector3 spawnPosition = Vector3.zero;
        bool foundValidSpawn = false;

        for (int attempt = 0; attempt < 100; attempt++)
        {
            float posX = UnityEngine.Random.Range(-spawnRange, spawnRange);
            float posY = UnityEngine.Random.Range(-spawnRange, spawnRange);
            spawnPosition = new Vector3(posX, 0, posY);

            bool farEnough = true;
            foreach (GameObject obstacle in obstacleObject)
            {
                if (Vector3.Distance(spawnPosition, obstacle.transform.position) < spawnCheckRadius)
                {
                    farEnough = false;
                    break;
                }
            }

            if (farEnough)
            {
                foundValidSpawn = true;
                break;
            }
        }

        if (!foundValidSpawn)
        {
            Debug.LogWarning("Could not find a valid spawn position after 100 attempts.");
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
                activeEnemies.Add(enemy); // Add to active list
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
        activeEnemies.Remove(gameObject); // Remove from active list
        PoolingManager.instance.DeActiveEnemyToPool(gameObject);
        enemiesToDeactivate++;

        // Check and spawn only once after all enemies have been deactivated
        StartCoroutine(HandleDeactivation());
    }

    private IEnumerator HandleDeactivation()
    {
        yield return new WaitForEndOfFrame(); // Wait for end of frame to ensure all deactivations are processed

        int currentAliveEnemies = activeEnemies.Count; // Use the active enemies list
        
        int enemiesToSpawn = amountAliveAtOneTime - currentAliveEnemies;

        // Limit the number of enemies to spawn based on the remaining count
        enemiesToSpawn = Mathf.Min(enemiesToSpawn, numberOfEnemiesLeftToSpawn);

        if (enemiesToSpawn > 0)
        {
            Debug.Log("Checking to spawn: Current alive enemies: " + currentAliveEnemies + ", Enemies to spawn: " + enemiesToSpawn);
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemies();
            }
        }

        enemiesToDeactivate = 0; // Reset after handling deactivation
    }

}