using System;
using TMPro;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    [Header("Level")]
    private int level;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject groundCircle;
    [SerializeField] private GameObject levelComponent;
    [SerializeField] private ParticleSystem levelUpEffect;
    private float height;
    private PlayerAnimation playerAnimation;
    private PlayerAttack playerAttack;
    public static event Action<GameObject> OnPlayerDeath;
    public static event Action<float> OnPlayerIncreaseHeight;
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
        height = GetComponent<CapsuleCollider>().height;
    }

    public void IsDead(GameObject enemy) {
        isAlive = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
        playerAnimation.SetDeadAnimation(true);
        this.gameObject.GetComponent<PlayerComponent>().SetDeadSkinMaterial();
        transform.position += new Vector3(0, 0.45f, 0);
        groundCircle.SetActive(false);
        levelComponent.SetActive(false);
        this.gameObject.layer = LayerMask.NameToLayer("Dead");
        OnPlayerDeath?.Invoke(enemy);
    }

    public void IncreaseLevel(int otherLevel) {
        if (level <= otherLevel) {
            level ++;
            IncreaseRange();
            levelText.text = $"{level}";
            levelUpEffect.Play();
            OnPlayerIncreaseHeight?.Invoke(height);
        }
    }

    public void IncreaseRange() {
        float currentAttackRange = playerAttack.GetAttackRange();
        playerAttack.SetAttackRange(currentAttackRange * 1.1f);
        this.gameObject.transform.localScale = new Vector3 ((float)(this.gameObject.transform.localScale.x * 1.1),
         (float)(this.gameObject.transform.localScale.y * 1.1), (float)(this.gameObject.transform.localScale.z * 1.1));
         height = Mathf.Round((height * 1.1f) * 10) / 10f;
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