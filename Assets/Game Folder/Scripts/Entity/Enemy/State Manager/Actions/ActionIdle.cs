using UnityEngine;

public class ActionIdle : FSM_Action
{
    // ---------------------------------- Attribute
    [Header("Animation Class")]
    private EnemyAnimation enemyAnimation;

    // ---------------------------------- Unity Functions
    void Awake()
    {
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
    }

    // ---------------------------------- User Defined Functions
    public override void Act()
    {
        enemyAnimation.SetIdleAnimation(true);
    }
}