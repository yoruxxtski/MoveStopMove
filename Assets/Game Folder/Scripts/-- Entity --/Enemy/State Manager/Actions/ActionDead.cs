using System;
using System.Collections;
using UnityEngine;

public class ActionDead : FSM_Action
{
    // -------------------------- Attribute
    private EnemyAnimation enemyAnimation;
    public static event Action OnEnemyDeath;

    // -------------------------- Unity Functions

    void Awake()
    {
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
    }

    // -------------------------- User Defined Functions
    public override void Act()
    {
        enemyAnimation.SetDeadAnimation(true);
        StartCoroutine(ReturnToPool());
    }
    IEnumerator ReturnToPool() {
        yield return new WaitForSeconds(2.0f);
        PoolingManager.instance.DeActiveEnemyToPool(gameObject);
        OnEnemyDeath?.Invoke();
    }

}