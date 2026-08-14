using UnityEngine;
using UnityEngine.InputSystem;

public abstract class EntityState
{
    protected string animBoolName;
    protected Player player;
    protected StateMachine stateMachine;
    
    protected Animator anim;
    protected Rigidbody2D rigidBody;
    protected PlayerInputSet playerInput;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.player = player;
        this.rigidBody = player.rigidBody;
        this.playerInput = player.input;
        this.anim = player.anim;
    }

    public virtual void Enter()
    {
        //everytime state will change, enter will be called
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        // anim.SetFloat("xVelocity", rigidBody.velocity.x);
        anim.SetFloat("yVelocity", rigidBody.linearVelocity.y);
    }

    public virtual void Exit()
    {
        // this will be called, everytime we exit state and change to a new one
        player.anim.SetBool(animBoolName, false);
    }
}