using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowSpawnManager : Singleton<ArrowSpawnManager>
{
    // --------------------------------- Attribute
    [SerializeField] private TagType arrowType;
    private List<GameObject> activeEnemies;
    private Dictionary<GameObject, GameObject> enemyArrowMap = new Dictionary<GameObject, GameObject>();
    // Reference to the Player_Stats component
    private Player_Stats playerStats;

    // ---------------------------------- Unity Functions
    void Start()
    {
        playerStats = FindObjectOfType<Player_Stats>();
    }
    void Update()
    {
        // Check if player is alive
        if (playerStats == null || !playerStats.GetAliveState())
        {
            DeactivateAllArrows();
            return;
        }

        activeEnemies = EnemySpawnManager.instance.GetListEnemies();
        
        // Check for each active enemy
        foreach (GameObject enemy in activeEnemies)
        {
            Vector3 enemyPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
            bool isOffscreen = enemyPos.x < 0 || enemyPos.x > Screen.width || enemyPos.y < 0 || enemyPos.y > Screen.height;

            // Spawn an arrow only if the enemy is offscreen and does not already have an arrow
            if (isOffscreen && !enemyArrowMap.ContainsKey(enemy))
            {
                SpawnArrow(enemy);
            }
            else if (!isOffscreen && enemyArrowMap.ContainsKey(enemy))
            {
                DeactivateArrow(enemy);
            }
        }
    }

    // ----------------------------------- User Defined Functions
    private void SpawnArrow(GameObject enemy)
    {
        Vector3 spawnPos = new Vector3(50, 0, 0); // Adjust Y position as needed
        
        GameObject arrow = PoolingManager.instance.SpawnFromPool(arrowType, spawnPos, Quaternion.identity);

        // Set the target enemy for the arrow movement
        ArrowMovement arrowMovement = arrow.GetComponent<ArrowMovement>();
        
        ArrowStats arrowStats = arrow.GetComponent<ArrowStats>();
        
        if (arrowStats != null) {

            EnemyComponent enemyComponent = enemy.GetComponent<EnemyComponent>();
            Enemy_Stats enemy_Stats = enemy.GetComponent<Enemy_Stats>();
            
            arrowStats.GetArrowImage().GetComponent<Image>().color = enemyComponent.skinMaterial.color;
            arrowStats.GetLevelImage().GetComponent<Image>().color = enemyComponent.skinMaterial.color;

            arrowStats.GetArrowLevel().text = $"{enemy_Stats.GetLevel()}";
        }

        if (arrowMovement != null)
        {
            arrowMovement.SetTargetEnemy(enemy);
        }

        // Store the arrow with the corresponding enemy
        enemyArrowMap[enemy] = arrow; 
    }

    public void DeactivateArrow(GameObject enemy)
    {
        if (enemyArrowMap.TryGetValue(enemy, out GameObject arrow))
        {
            PoolingManager.instance.DeActiveArrowToPool(arrow);
            enemyArrowMap.Remove(enemy); // Remove the entry from the map
        }
    }

    private void DeactivateAllArrows()
    {
        foreach (var arrow in enemyArrowMap.Values)
        {
            PoolingManager.instance.DeActiveArrowToPool(arrow);
        }
        enemyArrowMap.Clear();
    }
}
