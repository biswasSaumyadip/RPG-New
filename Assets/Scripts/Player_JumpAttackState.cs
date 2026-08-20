using UnityEngine;

public class Player_JumpAttackState : EntityState
{
    private bool touchedGround;
    public Player_JumpAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(
        player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        touchedGround = false;
        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDir, player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();
        if (player.isGrounded && !touchedGround)
        {
            touchedGround = true;
            anim.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, rigidBody.linearVelocity.y);
        }

        if (triggerCalled && player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
    
}
