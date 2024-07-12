using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private readonly int isIdle = Animator.StringToHash("IsIdle");
    private readonly int isAttack = Animator.StringToHash("IsAttack");
    private readonly int isDead = Animator.StringToHash("IsDead");
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetIdleAnimation(bool value) {
        animator.SetBool(isIdle, value);
    }
    public void SetAttackAnimation(bool value) {
        animator.SetBool(isAttack, value);
    }
    public void SetDeadAnimation(bool value) {
        animator.SetBool(isDead, value);
    }
}