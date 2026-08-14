using UnityEngine;

public class Player_FallState : Player_AiredState
{

    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }
        
        if(player.isWallDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
