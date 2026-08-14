using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string animBoolName) : base
    (player, stateMachine,
        animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, rigidBody.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput != Vector2.zero)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}