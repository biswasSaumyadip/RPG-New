using UnityEngine;

public class Player_JumpState : Player_AiredState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        //object go up, increase y velocity
        player.SetVelocity(rigidBody.linearVelocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        // Implement jump logic here
        if (rigidBody.linearVelocity.y < 0)
        {
            // Player is falling
            stateMachine.ChangeState(player.fallState);
        }
    }

}
