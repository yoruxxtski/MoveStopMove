using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{
    // -------------------------------- Attribute
    [Header("Tag")]
    [SerializeField] private TagType enemyTag;

    [Header("Alive State")]
    private bool checkAlive;
    private EnemyController enemyController;
    private ActionWander actionWander;

    // -------------------------------- Unity Functions
    void Awake()
    {
        checkAlive = true;
        enemyController = GetComponent<EnemyController>();
        actionWander = GetComponent<ActionWander>();
    }

    void OnEnable()
    {
        checkAlive = true;
        enemyController.ChangeState(StateType.WANDER);
        actionWander.ResetIdleFlag();
    }

    // -------------------------------- User Defined Functions

    // -------------------------------- Getter & Setter
    public bool GetAliveState() {
        return checkAlive;
    }
    public void SetAliveState(bool value) {
        checkAlive = value;
    }
    public TagType GetEnemyTag() {
        return enemyTag;
    }
}