using UnityEngine;

public abstract class EntityState
{
    protected Animator anim;
    protected string animBoolName;
    protected Player player;
    protected PlayerInputSet playerInput;
    protected Rigidbody2D rigidBody;
    protected StateMachine stateMachine;
    protected float stateTimer;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.player = player;
        rigidBody = player.rigidBody;
        playerInput = player.input;
        anim = player.anim;
    }

    public virtual void Enter()
    {
        //everytime state will change, enter will be called
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        // anim.SetFloat("xVelocity", rigidBody.velocity.x);
        stateTimer -= Time.deltaTime;
        anim.SetFloat("yVelocity", rigidBody.linearVelocity.y);

        if (playerInput.Player.Dash.WasPressedThisFrame() && CanDash())
            stateMachine.ChangeState(player.dashState);
    }

    public virtual void Exit()
    {
        // this will be called, everytime we exit state and change to a new one
        player.anim.SetBool(animBoolName, false);
    }

    private bool CanDash()
    {

        if(player.isWallDetected) return false;
        if(stateMachine.currentState == player.dashState) return false;
        return true;
    }
}