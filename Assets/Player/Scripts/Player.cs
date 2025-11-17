using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputSet Input { get; private set; }

    [Header("State control")]
    private StateMachine stateMachine;
    public EntityState IdleState { get; private set; }
    public EntityState MoveState { get; private set; }
    public EntityState JumpState { get; private set; }
    public EntityState FallState { get; private set; }
    public EntityState WallSlideState { get; private set; }
    public EntityState WallJumpState { get; private set; }
    public EntityState DashState { get; private set; }
    public EntityState BasicAttackState { get; private set; }

    [Header("Attack details")]
    public Vector2 attackVelocity;
    public float attackVelocityDuration = .1f;

    [Header("Movement details")]
    public Vector2 Movement { get; private set; }
    public float moveSpeed;
    public float jumpForce;
    [Range(0, 1)]
    public float inAirMoveMultiplier = .7f;
    [Range(0, 1)]
    public float WallGripMultiplier { get; private set; } = .3f;
    public int FacingDirection { get; private set; } = 1;
    private bool isFacingRight = true;
    public Vector2 WallJumpForce { get; private set; } = new(6, 12);


    [Space]
    public float dashDuration = .25f;
    public float DashForce { get; private set; } = 20f;

    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    public bool GroundDetected { get; private set; }
    public bool WallDetected { get; private set; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
        Input = new PlayerInputSet();

        stateMachine = new StateMachine();
        IdleState = new PlayerIdleState(this, stateMachine);
        MoveState = new PlayerMoveState(this, stateMachine);
        JumpState = new PlayerJumpState(this, stateMachine);
        FallState = new PlayerFallState(this, stateMachine);
        WallSlideState = new PlayerWallSlideState(this, stateMachine);
        WallJumpState = new PlayerWallJumpState(this, stateMachine);
        DashState = new PlayerDashState(this, stateMachine);
        BasicAttackState = new PlayerBasicAttackState(this, stateMachine);
    }

    private void OnEnable()
    {
        Input.Enable();

        Input.Player.Movement.performed += ctx => Movement = ctx.ReadValue<Vector2>();
        Input.Player.Movement.canceled += ctx => Movement = Vector2.zero;
    }

    private void OnDisable()
    {
        Input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(IdleState);
    }

    private void Update()
    {

        stateMachine.UpdateActiveState();
        HandleCollisionDetection();
    }

    public void CallAnimationTrigger()
    {

        stateMachine.CurrentState.CallAnimationTrigger();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {

        if ((xVelocity > 0 && !isFacingRight) || (xVelocity < 0 && isFacingRight))
            Flip();
    }

    public void Flip()
    {

        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
        FacingDirection *= -1;
    }

    private void HandleCollisionDetection()
    {
        GroundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        WallDetected = Physics2D.Raycast(transform.position, Vector2.right * FacingDirection, wallCheckDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * FacingDirection, 0f));
    }
}
