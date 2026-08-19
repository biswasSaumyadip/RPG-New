using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement details")] public float moveSpeed = 4f;

    public float jumpForce = 5f;
    public Vector2 wallJumpForce;

    [Range(0, 1)] public float inAirMoveMultiplier = 0.5f;

    [Range(0, 1)] public float wallSlideSlowMultiplier = 0.3f;

    [Space] public float dashDuration = 0.25f;

    public float dashSpeed = 20;

    [Header("Collision detection")] [SerializeField]
    private float groundCheckDistance;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float wallCheckDistance;
    private bool facingRight = true;

    [Header("Attack Details")] 
    public Vector2[] attackVelocity;

    public float attackVelocityDuration = .1f;
    
    public float comboResetTime = 1;
    private Coroutine queuedAttackCoroutine;
    
    
    public Animator anim { get; private set; }
    public PlayerInputSet input { get; private set; }
    private StateMachine stateMachine { get; set; }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }

    public Rigidbody2D rigidBody { get; private set; }
    public Vector2 moveInput { get; private set; }

    public int facingDir { get; private set; } = 1;
    public bool isGrounded { get; private set; }
    public bool isWallDetected { get; private set; }


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
        input = new PlayerInputSet();
        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");
        dashState = new Player_DashState(this, stateMachine, "dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        HandleCollisionDetection();
        stateMachine.updateActiveState();
    }
    
    public void EnterAttackStateWithDelay()
    {
        if (queuedAttackCoroutine != null)
            StopCoroutine(queuedAttackCoroutine);
        queuedAttackCoroutine = StartCoroutine(EnterAttackStateWithDelayCo());
    }

    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0,
            -groundCheckDistance));

        Gizmos.DrawLine(transform.position,
            transform.position + new Vector3(facingDir * wallCheckDistance, 0));
    }

    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
    }

    public void SetVelocity(float x, float y)
    {
        rigidBody.linearVelocity = new Vector2(x, y);
        HandleFlip(x);
    }

    private void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !facingRight)
            Flip();
        else if (xVelocity < 0 && facingRight) Flip();
    }

    public void Flip()
    {
        // Implementation for flipping the player sprite
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
        facingDir = facingRight ? 1 : -1;
    }

    private void HandleCollisionDetection()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance,
            whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir,
            wallCheckDistance,
            whatIsGround);
    }
}