using TMPro;
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
    private DecisionAttack decisionAttack;

    [Header("Level")]
    private int level;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject levelComponent;

    // -------------------------------- Unity Functions
    void Awake()
    {
        checkAlive = true;
        enemyController = GetComponent<EnemyController>();
        actionWander = GetComponent<ActionWander>();
        decisionAttack = GetComponent<DecisionAttack>();
    }

    void OnEnable()
    {
        checkAlive = true;
        enemyController.ChangeState(StateType.WANDER);
        actionWander.ResetIdleFlag();
        levelComponent.SetActive(true);
    }

    // -------------------------------- User Defined Functions
    public void SetLevelAccordingToPlayer(int playerLevel) {
        int minLevel = Mathf.Max(0, playerLevel - 3);
        int maxLevel = playerLevel + 3;
        // Randomly choose a level within the range
        level = Random.Range(minLevel, maxLevel + 1); // maxLevel + 1 because Random.Range's max is exclusive for integers
        levelText.text = $"{level}";
    }

    public void OriginalTranform() {
        TransformScale();
        TransformRange();
    }

    public void TransformScale() {
        if (level == 0) return;
        else {
            for (int i = 0; i < level; i++) {
                this.gameObject.transform.localScale *= 1.1f;
            }
        }
    }
    public void TransformRange() {
        if (level == 0) return;
        else {
                if (decisionAttack == null) return;
                float attackRange = decisionAttack.GetAttackRange();
                // Debug.Log($"Attack Range : {attackRange}");
                attackRange *= Mathf.Pow(1.1f, level); 
                decisionAttack.SetAttackRange(attackRange);
        }   
    }

    public void ResetTransform() {
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        decisionAttack.SetAttackRange(decisionAttack.GetOriginalAttackRange());
    }

    public void IncreaseLevel(int otherLevel) {
        if (level <= otherLevel) {
            level ++;
            IncreaseRange();
            levelText.text = $"{level}";
        }
    }

    public void IncreaseRange() {
        float currentAttackRange = decisionAttack.GetAttackRange();
        decisionAttack.SetAttackRange(currentAttackRange * 1.1f);

        this.gameObject.transform.localScale = new Vector3 ((float)(this.gameObject.transform.localScale.x * 1.1),
         (float)(this.gameObject.transform.localScale.y * 1.1), (float)(this.gameObject.transform.localScale.z * 1.1));
    }

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
    public int GetLevel() {
        return level;
    }
}