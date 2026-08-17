public class Player_DashState : EntityState
{
    private int dashDir;
    private float originalGravityScale;

    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(
        player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1;
        dashDir = player.facingDir;
        originalGravityScale = rigidBody.gravityScale;
        rigidBody.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();
        CancelDashIfNeeded();

        player.SetVelocity(player.dashSpeed * dashDir, 0);

        if (stateTimer < 0)
        {
            if (player.isGrounded)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
        rigidBody.gravityScale = originalGravityScale;
    }

    private void CancelDashIfNeeded()
    {
        if (player.isWallDetected)
        {
            if (player.isGrounded)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.wallSlideState);
        }
    }
}