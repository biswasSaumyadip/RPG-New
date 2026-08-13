using UnityEngine;

public class Player_MoveState : EntityState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string animBoolName) : base(
        player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput == Vector2.zero)
        {
            stateMachine.ChangeState(player.idleState);
        }
        
        player.SetVelocity(player.moveInput.x * player.moveSpeed, rigidBody.linearVelocity.y);
    }
}