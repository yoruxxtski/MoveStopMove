using System;
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

    private Player_Stats player_Stats;
    public static event Action OnEnemyDeath;

    //----------------------------------------- UNITY FUNCTIONS

    protected override void Awake()
    {
        base.Awake();
        player_Stats = FindObjectOfType<Player_Stats>();
        if (player_Stats == null) Debug.Log("Player Stats is null in enemySpawnManager");
    }
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

    void OnEnable()
    {
        Player_Stats.OnPlayerDeath += PlayerDeactive;
    }
    void OnDisable() 
    {
        Player_Stats.OnPlayerDeath -= PlayerDeactive;
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

    public void DeactivateEnemy(GameObject enemy)
    {
        // Notify ArrowSpawnManager to deactivate the arrow
        ArrowSpawnManager.instance.DeactivateArrow(enemy);

        PoolingManager.instance.DeActiveEnemyToPool(enemy);

        activeEnemies.Remove(enemy);

        numberOfHumanoidAlive --;

        OnEnemyDeath?.Invoke();
    }

    public void PlayerDeactive() {
        numberOfHumanoidAlive --;
        OnEnemyDeath?.Invoke();
    }

   public void SpawnEnemies()
    {
        if (numberOfEnemiesLeftToSpawn > 0 && activeEnemies.Count < amountAliveAtOneTime)
        {
            Vector3 spawnPos = GetRandomLocation();

            GameObject enemy = PoolingManager.instance.SpawnFromPool(enemyTag, spawnPos, Quaternion.identity);

            if (enemy != null)
            {
                Enemy_Stats enemy_Stats = enemy.GetComponent<Enemy_Stats>();


                Debug.Log(player_Stats.GetLevel());
                
                enemy_Stats.SetLevelAccordingToPlayer(player_Stats.GetLevel());

                enemy_Stats.OriginalTranform();

                enemy.layer = LayerMask.NameToLayer("Enemy");

                activeEnemies.Add(enemy);
                
                numberOfEnemiesLeftToSpawn--;
                // Debug.Log("Spawned enemy. Remaining: " + numberOfEnemiesLeftToSpawn);
            }
            else
            {
                Debug.LogWarning("Failed to spawn enemy. Pool might be empty.");
            }
        }
        else
        {
            Debug.LogWarning("No more enemies left to spawn or max active enemies reached.");
        }
    }
    // --------------------------------- Getter & Setter
    public List<GameObject> GetListEnemies() {
        return activeEnemies;
    }
    
    public int GetNumberOfHumanoidAlive() {
        return numberOfHumanoidAlive;
    }
}
