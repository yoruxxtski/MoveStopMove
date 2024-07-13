using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   // ---------------------------------- Attribute
   [Header("Attack Elements")]
   [SerializeField] private float attackRange;
   [SerializeField] private LayerMask enemyLayer;
   [SerializeField] private Transform attackPosition;
   private GameObject enemy;
   private GameObject previousEnemy;
   private float attackTimer;
   private float attackCoolDown;
   private bool canAttack = false;
   private PlayerMovement playerMovement;
   private PlayerAnimation playerAnimation;
   private PlayerComponent playerComponent;
   private Player_Stats player_Stats;

   // ---------------------------------- Unity Functions

   void Awake()
   {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        player_Stats = GetComponent<Player_Stats>();
        playerComponent = GetComponent<PlayerComponent>();
   }
   void Start()
   {
        attackCoolDown = 1.0f;
        attackTimer = attackCoolDown;
   }

   void Update()
   {
        if (player_Stats.GetAliveState()) {
            DetectEnemy();
            attackTimer -= Time.deltaTime;
            if (playerMovement.GetMovingState()) {
                canAttack = true;
            }
            if (!playerMovement.GetMovingState() && attackTimer <= 0f && enemy != null) {
                Attack();
            }
        } else {
            TurnOffAllDetection();
        }
        
   }

   void OnDrawGizmos()
   {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
   }

   // ---------------------------------- User Defined Functions
    public void DetectEnemy() {
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
    
        // Check if the current enemy is still within the detection range
        if (enemy != null) {
            bool currentEnemyStillDetected = false;
            foreach (Collider collider in hitColliders) {
                if (collider.gameObject == enemy) {
                    currentEnemyStillDetected = true;
                    break;
                }
            }
            
            if (currentEnemyStillDetected) {
                // Current enemy is still within range, no need to change
                return;
            } else {
                // Current enemy is out of range, deselect it
                enemy.GetComponent<EnemySelected>().ChangeSelectorSprite(false);
                previousEnemy = null;
                enemy = null;
            }
        }
        
        // If there are enemies in range and no current enemy, select the first one
        if (hitColliders.Length > 0) {
            enemy = hitColliders[0].gameObject;
            if (enemy != previousEnemy) {
                enemy.GetComponent<EnemySelected>().ChangeSelectorSprite(true);
                previousEnemy = enemy;
            }
        }
    }

    public void TurnOffAllDetection() {
        if (enemy != null)
        enemy.GetComponent<EnemySelected>().ChangeSelectorSprite(false);
        previousEnemy = null;
        enemy = null;
    }

    public void Attack() {
        if (enemy != null) {
            transform.LookAt(enemy.transform);

            if (canAttack) {
                playerAnimation.SetAttackAnimation(true);
                // Take current weapon
                Weapon currentWeapon = playerComponent.GetCurrentWeapon();
                // Get the tag type
                TagType tag = currentWeapon.tagType;
                // Active the projectile prefab
                GameObject projectile = PoolingManager.instance.SpawnFromPool(tag, attackPosition.position, Quaternion.identity);

                Vector3 targetPosition = enemy.transform.position;
                
                // Set the direction of the projectile
                if (projectile != null) {

                    Projectile projectileComponent = projectile.GetComponent<Projectile>();
                    Vector3 direction;
                    if (projectileComponent != null)
                    {
                        direction = (targetPosition - attackPosition.position).normalized;
                        direction.y = 0;

                        projectileComponent.direction = direction;
                        projectileComponent.attackRange = attackRange;
                        projectileComponent.playerTransform = transform.position;
                        projectileComponent.rootParent = gameObject;
                        projectileComponent.currentWeapon = currentWeapon;
                    }
                }
                canAttack = false;
                StartCoroutine(WaitBeforeAttack());
            }
        }
    }

     private IEnumerator WaitBeforeAttack()
    {
        yield return new WaitForSeconds(0.5f); 
        playerAnimation.SetAttackAnimation(false);
        attackTimer = attackCoolDown; 
    }

    public float GetAttackRange() {
        return attackRange;
    }

    public void SetAttackRange(float value) {
        attackRange = value;
    } 
}
