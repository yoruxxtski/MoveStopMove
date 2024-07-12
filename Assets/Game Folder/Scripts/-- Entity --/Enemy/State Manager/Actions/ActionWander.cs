using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ActionWander : FSM_Action
    // TODO : Upgrade the action wander 
{
    // --------------------------------- Attribute
    [Header("Animation")]
    private EnemyAnimation enemyAnimation;
    private Enemy_Stats enemy_Stats;

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

    // ----------------------------------
    private bool isIdling = false;
    private bool hasWandered = false;

    // --------------------------------- Unity Functions

    void Awake()
    {
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        enemy_Stats = GetComponentInChildren<Enemy_Stats>();
        agent = GetComponent<NavMeshAgent>();
    }
    // --------------------------------- User Defined Functions
   
    public override void Act()
    {
        if (!enemy_Stats.GetAliveState()) {
            return;
        }

        if (!walkPointSet && !isIdling) {
            SearchWalkPoint();
        }
        if (walkPointSet) {
            StartWandering();
        }
    }
    private void StartWandering()
    {
        enemyAnimation.SetIdleAnimation(false);
        agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            agent.SetDestination(transform.position);
            walkPointSet = false;
            isIdling = true;
            hasWandered = true;
            StartCoroutine(StartIdling());
        }
    }

    private IEnumerator StartIdling()
    {
        enemyAnimation.SetIdleAnimation(true);
        yield return new WaitForSeconds(2.0f);
        isIdling = false;
        walkPointSet = false;
    }

    public void SearchWalkPoint() {
        // Calculate random direction and distance
        float randomDistance = Random.Range(minWalkDistance, walkPointRange);
        float randomAngle = Random.Range(0, Mathf.PI * 2); // Random angle in radians

        // Calculate random point in range
        float randomX = Mathf.Cos(randomAngle) * randomDistance;
        float randomZ = Mathf.Sin(randomAngle) * randomDistance;

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, - transform.up , 2f ,groundMask)) {
            walkPointSet = true;
        }
    }

    // ------------------------------------------ Getter & Setter
    public bool HasWandered() {
        return hasWandered;
    }

    public void ResetWanderedFlag() {
        hasWandered = false;
    }
    public void ResetIdleFlag() {
        isIdling = false;
    }
    public void ResetWanderSetState() {
        walkPointSet = false;
    }
}