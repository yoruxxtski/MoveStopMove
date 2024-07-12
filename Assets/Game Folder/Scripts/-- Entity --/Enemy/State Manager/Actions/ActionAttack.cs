using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ActionAttack : FSM_Action
{
    // ---------------------------------- Attribute
    [Header("Animation")]
    private EnemyAnimation enemyAnimation;
    private ActionWander actionWander;
    private DecisionAttack decisionAttack;
    private EnemyComponent enemyComponent;
    // ---------------------------------- Attack
    [Header("Attack")]
    [SerializeField] private Transform attackPosition;

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
        enemyComponent = GetComponentInChildren<EnemyComponent>();
    }

    // ---------------------------------- User Defined Functions
    public override void Act()
    {
        // * First enter can't attack ; after wander then I can attack
        if (canAttack) {
            agent.SetDestination(transform.position);;
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

        Weapon currentWeapon = enemyComponent.GetCurrentWeapon();

        TagType tag = currentWeapon.tagType;
        if (this.gameObject != null) {
            
            GameObject projectile = PoolingManager.instance.SpawnFromPool(tag, attackPosition.position, Quaternion.identity);
            // Set the direction of the projectile
            if (projectile != null) {
                Projectile projectileComponent = projectile.GetComponent<Projectile>();
                Vector3 direction;
                if (projectileComponent != null)
                {
                    direction = (targetPosition - attackPosition.position).normalized;
                    direction.y = 0;

                    projectileComponent.direction = direction;
                    projectileComponent.attackRange = decisionAttack.GetAttackRange();
                    projectileComponent.playerTransform = transform.position;
                    projectileComponent.rootParent = gameObject;
                    projectileComponent.currentWeapon = currentWeapon;
                }
            }
        }
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