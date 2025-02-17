using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PoolingManager : Singleton<PoolingManager>
{
    [System.Serializable]
    public class Pool {
        public TagType tag;
        public GameObject prefab;
        public int size;
        public Transform container;
    }
    // -----------------------------------

    public List<Pool> pools;
    public Dictionary<TagType, Queue<GameObject>> poolDictionary;

    // ------------------------------------- UNITY FUNCTIONS

    protected override void Awake()
    {
        base.Awake();
        InitializePool();
    }

    // ---------------------------------------------- USER DEFINED FUNCTIONS

    public void InitializePool() {
        poolDictionary = new Dictionary<TagType, Queue<GameObject>>();
        foreach (Pool pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab, pool.container); // 
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
        Debug.Log("PoolingManager initialized.");
    }
    public GameObject SpawnFromPool(TagType tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log($"No pool dictionary with tag: {tag}");
            return null;
        }

        if (poolDictionary[tag].Count == 0)
        {
            Debug.Log($"Pool with tag {tag} is empty. Consider increasing your pool size.");
            return null; // Not enough objects in the pool
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;
        return objToSpawn; // Don't enqueue here; handle in deactivation
    }

    public void DeactivateWeaponToPool(GameObject obj) {
        obj.SetActive(false); // Deactivate the GameObject
    
        // Assuming obj has a component like Projectile with a tagType property
        TagType tag = obj.GetComponent<Projectile>().currentWeapon.tagType;
    
        // Check if the pool dictionary contains the tag type
        if (poolDictionary.ContainsKey(tag)) {
            // Enqueue the object back into the correct pool
            poolDictionary[tag].Enqueue(obj);
        } else {
            Debug.LogWarning("Pool for tag type " + tag + " not found.");
            // Optionally, handle this case (e.g., instantiate a new pool)
        }
    }

    public void DeActiveEnemyToPool(GameObject obj) {
        obj.SetActive(false); // Deactivate the GameObject

        // Assuming the enemy GameObject has a component that holds its tag
        TagType tag = obj.GetComponent<Enemy_Stats>().GetEnemyTag(); 

        // Check if the pool dictionary contains the tag type
        if (poolDictionary.ContainsKey(tag)) {
            // Enqueue the object back into the correct pool
            poolDictionary[tag].Enqueue(obj);
        } else {
            Debug.LogWarning("Pool for tag type " + tag + " not found.");
            // Optionally, handle this case (e.g., instantiate a new pool)
        }
    }

     public void DeActiveArrowToPool(GameObject obj) {
        obj.SetActive(false); // Deactivate the GameObject

        // Assuming the enemy GameObject has a component that holds its tag
        TagType tag = obj.GetComponent<ArrowMovement>().GetArrowTag(); 

        // Check if the pool dictionary contains the tag type
        if (poolDictionary.ContainsKey(tag)) {
            // Enqueue the object back into the correct pool
            poolDictionary[tag].Enqueue(obj);
        } else {
            Debug.LogWarning("Pool for tag type " + tag + " not found.");
            // Optionally, handle this case (e.g., instantiate a new pool)
        }
    }
    
}
