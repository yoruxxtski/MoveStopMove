using UnityEngine;
using UnityEngine.AI;

public class ActionWander : FSM_Action
    // TODO : Upgrade the action wander 
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
    [SerializeField] private float minWalkDistance;
    [SerializeField] private float walkPointRange;

    // --------------------------------
    [Header("Check Idle")]
    private bool turnIdle;
    
    // --------------------------------- Unity Functions

    void Awake()
    {
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        agent = GetComponent<NavMeshAgent>();
    }

    // --------------------------------- User Defined Functions
    
    public override void Act()
    {
        if (!walkPointSet && !turnIdle) 
        {
            SearchWalkPoint();
        }

        if (walkPointSet) {
            enemyAnimation.SetIdleAnimation(false);
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;    
        // walkPoint reached
        if (distanceToWalkPoint.magnitude < 1f && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance) {
            walkPointSet = false;
            turnIdle = true;
        }   
    }

    public void SearchWalkPoint() {
        // Calculate random direction and distance
        float randomDistance = Random.Range(minWalkDistance, walkPointRange);
        float randomAngle = Random.Range(0, Mathf.PI * 2); // Random angle in radians

        // Calculate random point in range
        float randomX = Mathf.Cos(randomAngle) * randomDistance;
        float randomZ = Mathf.Sin(randomAngle) * randomDistance;

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, - transform.up, 2f, groundMask)) {
            walkPointSet = true;
        }
    }

    public bool GetIdleState() {
        return turnIdle;
    }
    public void SetIdleState(bool value) {
        turnIdle = value;
    }
}