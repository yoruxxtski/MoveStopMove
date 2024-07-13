using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionDead : FSM_Action
{
     // -------------------------- Attribute
    private EnemyAnimation enemyAnimation;

    // public static event Action OnEnemyDeath;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject levelComponent;

    // -------------------------- Unity Functions

    void Awake()
    {
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
    }

    // -------------------------- User Defined Functions
    public override void Act()
    {
        agent.SetDestination(transform.position);
        enemyAnimation.SetDeadAnimation(true);
        if (levelComponent.activeSelf)
        levelComponent.SetActive(false);
        StartCoroutine(ReturnToPool());
    }
    IEnumerator ReturnToPool() {
        yield return new WaitForSeconds(1.5f);
        EnemySpawnManager.instance.DeactivateEnemy(this.gameObject);
        EnemySpawnManager.instance.SpawnEnemies(); // Spawn a new enemy immediately after deactivation
    }
}