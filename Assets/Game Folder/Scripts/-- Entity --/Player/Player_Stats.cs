using TMPro;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    [Header("Level")]
    private int level;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject groundCircle;
    [SerializeField] private GameObject levelComponent;
    private PlayerAnimation playerAnimation;
    private PlayerAttack playerAttack;

    private bool isAlive;

    void Awake()
    {
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Start()
    {
        level = 0;
        levelText.text = $"{level}";
        isAlive = true;
    }

    public void IsDead() {
        isAlive = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
        playerAnimation.SetDeadAnimation(true);
        transform.position += new Vector3(0, 0.45f, 0);
        groundCircle.SetActive(false);
        levelComponent.SetActive(false);
        this.gameObject.layer = LayerMask.NameToLayer("Dead");
    }

    public void IncreaseLevel(int otherLevel) {
        if (level < otherLevel) {
            level ++;
            IncreaseRange();
            levelText.text = $"{level}";
        }
    }

    public void IncreaseRange() {
        float currentAttackRange = playerAttack.GetAttackRange();
        playerAttack.SetAttackRange(currentAttackRange * 1.1f);
        this.gameObject.transform.localScale = new Vector3 ((float)(this.gameObject.transform.localScale.x * 1.1),
         (float)(this.gameObject.transform.localScale.y * 1.1), (float)(this.gameObject.transform.localScale.z * 1.1));
    }

    public int GetLevel() {
        return level;
    }

    public bool GetAliveState() {
        return isAlive;
    }
    public void SetAliveState(bool value) {
        isAlive = value;
    }
}