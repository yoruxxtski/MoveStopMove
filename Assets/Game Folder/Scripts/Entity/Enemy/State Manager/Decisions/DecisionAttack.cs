using UnityEngine;

public class DecisionAttack : FSM_Decision
{
    // --------------------------- Attribute
    [Header("LayerMask")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask playerMask;
    // ---------------------------
    [Header("Elements")]
    [SerializeField] private float attackRange;
    // ---------------------------
    [Header("GameObject Collided")]
    private GameObject playerCollided;
    private GameObject enemyCollided;

    [Header("Wander & Attack Class")]
     private ActionWander actionWander;
    private ActionAttack actionAttack;

    // --------------------------- Unity Functions
    void Awake()
    {
        actionWander = GetComponent<ActionWander>();
        actionAttack = GetComponent<ActionAttack>();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    // --------------------------- User Defined Functions
    public override bool Decide()
    {
        if (actionWander.HasWandered() && CheckEnemiesAndPlayerInRange())
        {
            actionAttack.AllowAttack();
            actionWander.ResetWanderedFlag();
            return true;
        }
        return false;
    }

    public bool CheckEnemiesAndPlayerInRange() {
         // * Enemy Found Player
        Collider[] playerCollider = Physics.OverlapSphere(transform.position, attackRange, playerMask);
        if (playerCollider.Length > 0) {
            playerCollided = playerCollider[0].gameObject;          
            return true;
        }
        // * Enemy Found Another Enemy
        Collider[] enemiesCollider = Physics.OverlapSphere(transform.position, attackRange, enemyMask);
        if (enemiesCollider.Length > 0) {
            foreach (Collider enemyCollider in enemiesCollider) {
                if (enemyCollider.gameObject != this.gameObject) {
                    enemyCollided = enemyCollider.gameObject;
                    Debug.Log($"Found another enemy : {enemyCollided}");
                    return true;
                }
            }
        }
        return false;
    }

     public float GetAttackRange() {
        return attackRange;
    }
    public void SetAttackRange(float value) {
        attackRange = value;
    }
    public GameObject GetPlayerCollided() {
        return playerCollided;
    }
    public GameObject GetEnemyCollided() {
        return enemyCollided;
    }
}