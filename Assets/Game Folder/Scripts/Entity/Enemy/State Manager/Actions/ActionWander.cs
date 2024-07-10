using UnityEngine;
using UnityEngine.AI;

public class ActionWander : FSM_Action
{
    // --------------------------------- Attribute
    [Header("Animation")]
    private EnemyAnimation enemyAnimation;
    // ---------------------------------
    [Header("NavMeshAgent Component")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask groundMask;
    // ---------------------------------
    [Header("Walk Point Elements")]
    private Vector3 walkPoint;
    private bool walkPointSet;
    [SerializeField] private float walkPointRange;
    
    // --------------------------------- Unity Functions

    void Awake()
    {
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        agent = GetComponent<NavMeshAgent>();
    }

    // --------------------------------- User Defined Functions
    
    public override void Act()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) {
            enemyAnimation.SetIdleAnimation(false);
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;    
        // walkPoint reached
        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
        }   
    }

    public void SearchWalkPoint() {
        // Calculate random point in range
        float randomX = Random.Range(- walkPointRange, walkPointRange);
        float randomZ = Random.Range(- walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, - transform.up, 2f, groundMask)) {
            walkPointSet = true;
        }
    }
}