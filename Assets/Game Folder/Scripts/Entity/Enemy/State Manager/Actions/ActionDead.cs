using System.Collections;
using UnityEngine;

public class ActionDead : FSM_Action
{
    // -------------------------- Attribute
    private Enemy_Stats enemy_Stats;
    private EnemyAnimation enemyAnimation;

    // -------------------------- Unity Functions

    void Awake()
    {
        enemy_Stats = GetComponent<Enemy_Stats>();
        enemyAnimation = GetComponent<EnemyAnimation>();
    }

    // -------------------------- User Defined Functions
    public override void Act()
    {
        enemyAnimation.SetDeadAnimation(true);
        StartCoroutine(ReturnToPool());
    }
    IEnumerator ReturnToPool() {
        yield return new WaitForSeconds(2.0f);
        // PoolingManager.instance.DeactivateToPool(gameObject);
    }
}