using UnityEngine;

public abstract class EntityState
{
    protected string animBoolName;
    protected Player player;
    protected StateMachine stateMachine;
    protected Rigidbody2D rigidBody;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.player = player;
        this.rigidBody = player.rigidBody;
    }

    public virtual void Enter()
    {
        //everytime state will change, enter will be called
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        //we going to run logic of the state here
        Debug.Log("Updating " + animBoolName);
    }

    public virtual void Exit()
    {
        // this will be called, everytime we exit state and change to a new one
        player.anim.SetBool(animBoolName, false);
    }
}