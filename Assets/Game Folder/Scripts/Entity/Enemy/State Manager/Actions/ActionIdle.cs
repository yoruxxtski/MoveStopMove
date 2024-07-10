using UnityEngine;

public class ActionIdle : FSM_Action
{
    // ---------------------------------- Attribute
    [Header("Animation Class")]
    private EnemyAnimation enemyAnimation;
    private ActionWander actionWander;

    // ---------------------------------- Unity Functions
    void Awake()
    {
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        actionWander = GetComponent<ActionWander>();
    }

    // ---------------------------------- User Defined Functions
    public override void Act()
    {
        enemyAnimation.SetIdleAnimation(true);
    }

    
}