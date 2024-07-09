using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    //-------------------------------------- Attributes
    [Header("Enemies Elements")]
    [SerializeField] private int totalNumberOfEnemies; // * Tong so luong quai vat (ban dau = 99)
    private int numberOfHumanoidAlive; // * Tong so luong dang con song (phai + 1 vi nguoi cung tinh la alive) (ban dau = 100)
    [SerializeField] private int amountAliveAtOneTime; // * So luong quai vat dc spawn tai 1 thoi diem (tran san luc nao cung se chi co 10 enemies)
    private int numberOfEnemiesLeftToSpawn; // * So luong quai vat con lai co the spawn (so  luong quai vat co the spawn (ban dau = 89))
    // ------------------------------------
    [Header("Spawning Enemies Elements")]
    [SerializeField] private string enemyTag; 
    [SerializeField] private int spawnRange;
    [SerializeField] private float spawnCheckRadius; // * Check de spawn ben canh se ko co object 

    //--------------------------------------
    [Header("Objects Not To Spawn On")]
    [SerializeField] private List<GameObject> obstacleObject;

    //----------------------------------------- UNITY FUNCTIONS
    void Start()
    {
        numberOfHumanoidAlive = totalNumberOfEnemies + 1; 
        numberOfEnemiesLeftToSpawn = totalNumberOfEnemies - amountAliveAtOneTime;

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

    public void SpawnEnemies() {
        if (numberOfEnemiesLeftToSpawn > 0) {
            Vector3 spawnPos = GetRandomLocation();
            PoolingManager.instance.SpawnFromPool(enemyTag, spawnPos, Quaternion.identity);
            numberOfEnemiesLeftToSpawn --;
        }
    }
}