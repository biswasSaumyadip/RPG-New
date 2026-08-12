using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(Player player,StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.stateName =  stateName;
        this.player = player;
    }

    public virtual void Enter()
    {
        //everytime state will change, enter will be called
        Debug.Log("Entering " + stateName);
    }

    public virtual void Update()
    {
        //we going to run logic of the state here
        Debug.Log("Updating " + stateName);
    }

    public virtual void Exit()
    {
        // this will be called, everytime we exit state and change to a new one
        Debug.Log("Exiting " + stateName);
    }
}
