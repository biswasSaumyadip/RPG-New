using UnityEngine;

public class Player_AiredState : EntityState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //object go up, increase y velocity

        // Implement fall logic here
    }
    
    public override void Update()
    {
        base.Update();

        // Implement fall logic here
        //if y velocity goes down then player is falling

        if (player.moveInput.x != 0)
        {
            player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.inAirMoveMultiplier), rigidBody
                .linearVelocity.y);
        }
    }
}
