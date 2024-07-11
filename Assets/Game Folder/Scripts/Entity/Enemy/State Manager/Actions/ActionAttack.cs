using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ActionAttack : FSM_Action
{
    // ---------------------------------- Attribute
    [Header("Animation")]
    private EnemyAnimation enemyAnimation;
    private ActionWander actionWander;
    private DecisionAttack decisionAttack;
    // ----------------------------------

    [Header("Nav Mesh Agent")]
    [SerializeField] private NavMeshAgent agent;

    // ----------------------------------
    [Header("Attacking Fied")]
    private bool canAttack = true;

    // ---------------------------------- Unity Functions
    void Awake()
    {
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        actionWander = GetComponentInChildren<ActionWander>();
        decisionAttack = GetComponentInChildren<DecisionAttack>();
    }

    // ---------------------------------- User Defined Functions
    public override void Act()
    {
        // * First enter can't attack ; after wander then I can attack
        if (canAttack) {
            agent.isStopped = true;
            Attack();
        }
    }
    private void Attack() {
        
        enemyAnimation.SetAttackAnimation(true);
        GameObject playerCollided = decisionAttack.GetPlayerCollided();
        GameObject enemyCollided = decisionAttack.GetEnemyCollided();

        Vector3 targetPosition = playerCollided != null ? playerCollided.transform.position : enemyCollided.transform.position;
        transform.LookAt(targetPosition);

        
        StartCoroutine(GoBackToWander());
    }

    IEnumerator GoBackToWander() {
        yield return new WaitForSeconds(1f);
        canAttack = false;
        actionWander.ResetIdleFlag();
        actionWander.ResetWanderSetState();
        enemyAnimation.SetAttackAnimation(false);
        agent.isStopped = false;
        
    }
    // --------------------------------- Getter & Setter
    public void AllowAttack() {
        canAttack = true;
    }

    public bool CanAttack() {
        return canAttack;
    }
}