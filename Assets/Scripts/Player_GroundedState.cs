public class Player_GroundedState : EntityState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) :
        base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (rigidBody.linearVelocity.y < 0 && !player.isGrounded)
            stateMachine.ChangeState(player.fallState);

        if (player.input.Player.Jump.WasPressedThisFrame())
            // Transition to jump state
            stateMachine.ChangeState(player.jumpState);
    }
}