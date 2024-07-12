using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawnManager : Singleton<ArrowSpawnManager>
{
    // --------------------------------- Attribute
    [SerializeField] private TagType arrowType;
    private List<GameObject> activeEnemies;
    private Dictionary<GameObject, GameObject> enemyArrowMap = new Dictionary<GameObject, GameObject>();

    // ---------------------------------- Unity Functions
    void Update()
    {
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
}
