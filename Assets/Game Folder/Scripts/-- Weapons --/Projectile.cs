using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 8f;
    public Vector3 direction {get; set;}
    public Vector3 playerTransform {get; set;}
    public float attackRange{get; set;}
    public GameObject rootParent {get; set;}
    public Weapon currentWeapon;

    void Start()
    {
        
        transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (currentWeapon.weaponType == WeaponType.Rotate) {
            transform.Rotate(- Vector3.forward, 5);
        }

        if (Vector3.Distance(this.transform.position, playerTransform) > (attackRange + 2)) {
            PoolingManager.instance.DeactivateWeaponToPool(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == null) Debug.Log("Other is null");

        // Deactive the weapon projectile

        if (other.CompareTag("Enemy") && rootParent.CompareTag("Player")) {

            PoolingManager.instance.DeactivateWeaponToPool(this.gameObject);

             // -- Other : nguoi bi nem

            Enemy_Stats enemy_Stats = other.gameObject.GetComponent<Enemy_Stats>();
            
            enemy_Stats.SetAliveState(false);
            
            enemy_Stats.gameObject.transform.position += new Vector3(0, 0.5f, 0);

            // Change layer of that enemy
            other.gameObject.layer = LayerMask.NameToLayer("Dead");

            Player_Stats player_Stats = rootParent.gameObject.GetComponent<Player_Stats>();

            player_Stats.IncreaseLevel(enemy_Stats.GetLevel());
            
        } 

        if (other.CompareTag("Enemy") && rootParent.CompareTag("Enemy")) {

            if (rootParent.gameObject == other.gameObject) {
                return;
            }

            PoolingManager.instance.DeactivateWeaponToPool(this.gameObject);

            // -- Other : nguoi bi nem

            Enemy_Stats enemy_Stats = other.gameObject.GetComponent<Enemy_Stats>();
            
            enemy_Stats.SetAliveState(false);
            
            enemy_Stats.gameObject.transform.position += new Vector3(0, 0.5f, 0);

            // Change layer of that enemy
            other.gameObject.layer = LayerMask.NameToLayer("Dead");

            // -- Root Parent = nguoi nem

            Enemy_Stats enemy_Stats_Root = rootParent.gameObject.GetComponent<Enemy_Stats>();

            enemy_Stats_Root.IncreaseLevel(enemy_Stats.GetLevel());
        }
        if (other.CompareTag("Player")) {

            PoolingManager.instance.DeactivateWeaponToPool(this.gameObject);

            Player_Stats player_Stats = other.gameObject.GetComponent<Player_Stats>();

            player_Stats.IsDead(rootParent);

            Enemy_Stats enemy_Stats = rootParent.gameObject.GetComponent<Enemy_Stats>();

            enemy_Stats.IncreaseLevel(player_Stats.GetLevel());
        }
    }
}