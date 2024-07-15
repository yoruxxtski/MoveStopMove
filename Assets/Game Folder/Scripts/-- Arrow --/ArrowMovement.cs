using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    
    [SerializeField] private TagType tagType;
    [SerializeField] private Transform rotateArrow;
    public GameObject targetEnemy; // Reference to the enemy the arrow is pointing to

    private void Update()
    {
        if (targetEnemy != null)
        {
            // // Get the screen position of the enemy
            // Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetEnemy.transform.position);

            // // Clamp the arrow's position to stay within screen bounds
            // Vector3 cappedScreenPos = screenPosition;
            // cappedScreenPos.x = Mathf.Clamp(cappedScreenPos.x, 80, Screen.width - 80);
            // cappedScreenPos.y = Mathf.Clamp(cappedScreenPos.y, 80, Screen.height - 80);

            // // Move the arrow to the clamped screen position
            // transform.position = cappedScreenPos;
            
            // // Calculate the direction to the enemy
            //  // Calculate the direction to the enemy relative to the screen center
            // Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            // Vector3 direction = screenPosition - screenCenter;

            // direction.z = 0; // Keep it in the 2D plane

            // // Calculate the angle for rotation
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // if (rotateArrow != null)
            // {
            //     rotateArrow.rotation = Quaternion.Euler(0, 0, angle - 90);
            // }
            // else
            // {
            //     Debug.Log("Can't find rotate arrow");
            // }
            // Get the screen position of the enemy
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetEnemy.transform.position);

            // Calculate the direction to the enemy relative to the screen center
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Vector3 direction = screenPosition - screenCenter;
            direction.z = 0; // Keep it in the 2D plane

            // Calculate the angle for rotation
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (rotateArrow != null)
            {
                rotateArrow.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
            else
            {
                Debug.Log("Can't find rotate arrow");
            }

            // Clamp the arrow's position to stay within screen bounds
            Vector3 cappedScreenPos = screenPosition;
            cappedScreenPos.x = Mathf.Clamp(cappedScreenPos.x, 80, Screen.width - 80);
            cappedScreenPos.y = Mathf.Clamp(cappedScreenPos.y, 80, Screen.height - 80);

            // Move the arrow to the clamped screen position
            transform.position = cappedScreenPos;
        }
    }

    public TagType GetArrowTag()
    {
        return tagType;
    }

    // Optional: Method to set the target enemy
    public void SetTargetEnemy(GameObject enemy)
    {
        targetEnemy = enemy;
    }
}