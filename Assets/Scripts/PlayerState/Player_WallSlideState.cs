public class Player_WallSlideState : EntityState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) :
        base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        if (playerInput.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.wallJumpState);

        if (!player.isWallDetected) stateMachine.ChangeState(player.fallState);

        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
            player.Flip();
        }
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
            player.SetVelocity(player.moveInput.x, rigidBody.linearVelocity.y);
        else
            player.SetVelocity(player.moveInput.x,
                rigidBody.linearVelocity.y * player.wallSlideSlowMultiplier);
    }
}