using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PoolingManager : Singleton<PoolingManager>
{
    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
        public Transform container;
    }
    // -----------------------------------

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // ------------------------------------- UNITY FUNCTIONS

    protected override void Awake()
    {
        base.Awake();
        InitializePool();
    }

    // ---------------------------------------------- USER DEFINED FUNCTIONS

    public void InitializePool() {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
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
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) {
        if (!poolDictionary.ContainsKey(tag)) {
            Debug.Log($"No pool dictionary with tag : {tag}");
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        
        objToSpawn.SetActive(true);

        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objToSpawn);
        return objToSpawn;
    }
    
}
