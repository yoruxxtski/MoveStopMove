using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private DynamicJoystick dynamicJoystick;
    private Rigidbody rigidBody;

    [Header("Animation")]
    private PlayerAnimation playerAnimation;
    private Player_Stats player_Stats;

    [Header("Movements")]
    [SerializeField] private float speed;
    private Vector3 MoveDir;
    float MyFloat;

    private bool isMoving = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        player_Stats = GetComponent<Player_Stats>();
    }

    void Update()
    {
        ReadMovement();
    }
    void FixedUpdate()
    {
        if (player_Stats.GetAliveState()) 
        Move();
    }
    public void ReadMovement() {
        float horizontalInput = dynamicJoystick.Horizontal;
        float verticalInput = dynamicJoystick.Vertical;
        MoveDir = new Vector3(horizontalInput, 0, verticalInput);
    }
    public void Move() {
        // move
        if (MoveDir == Vector3.zero) {
            playerAnimation.SetIdleAnimation(true);
            isMoving = false;
            return;
        }
        rigidBody.MovePosition(transform.position + MoveDir.normalized * speed * Time.deltaTime);
        
        // rotate
        float angel = Mathf.Atan2(MoveDir.x, MoveDir.z) * Mathf.Rad2Deg;
        float smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, angel, ref MyFloat, 0.1f);
        transform.rotation = Quaternion.Euler(0, smooth, 0);
        playerAnimation.SetIdleAnimation(false);
        isMoving = true;
    }

    public bool GetMovingState() {
        return isMoving;
    }
}