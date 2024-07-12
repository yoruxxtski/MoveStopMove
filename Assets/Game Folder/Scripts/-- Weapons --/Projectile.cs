using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 7f;
    public Vector3 direction {get; set;}
    public Vector3 playerTransform {get; set;}
    public float attackRange{get; set;}
    public GameObject rootParent {get; set;}
    public Weapon currentWeapon;
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(this.transform.position, playerTransform) > (attackRange + 2)) {
            PoolingManager.instance.DeactivateWeaponToPool(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == null) Debug.Log("Other is null");

        // Deactive the weapon projectile

        if (other.CompareTag("Enemy") && rootParent.CompareTag("Player")) {
            
        } 

        if (other.CompareTag("Enemy") && rootParent.CompareTag("Enemy")) {
            PoolingManager.instance.DeactivateWeaponToPool(this.gameObject);

            Enemy_Stats enemy_Stats = other.gameObject.GetComponent<Enemy_Stats>();
            enemy_Stats.SetAliveState(false);
            // Change layer of that enemy
            other.gameObject.layer = LayerMask.NameToLayer("Dead");
        }
        if (other.CompareTag("Player")) {
            
        }
    }
}