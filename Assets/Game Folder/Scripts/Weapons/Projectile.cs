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

        if (other.CompareTag("Enemy") && rootParent.CompareTag("Player")) {
            Debug.Log("Player projectile hit enemy");
            
        } 

        if (other.CompareTag("Enemy") && rootParent.CompareTag("Enemy")) {
            PoolingManager.instance.DeactivateWeaponToPool(this.gameObject);
          
        }
        if (other.CompareTag("Player")) {
            PoolingManager.instance.DeactivateWeaponToPool(this.gameObject);
        }
    }
}