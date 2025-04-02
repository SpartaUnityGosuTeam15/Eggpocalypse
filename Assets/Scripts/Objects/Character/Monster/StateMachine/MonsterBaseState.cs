using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBaseState : IState
{
    protected MonsterStateMachine stateMachine;

    public MonsterBaseState(MonsterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
        if (stateMachine.Monster == null) return;
        stateMachine.Monster.Agent.SetDestination(stateMachine.Target.transform.position);
    }

    protected void StartAnimation(int hash)
    {
        stateMachine.Monster.Animator.SetBool(hash, true);
    }

    protected void StopAnimation(int hash)
    {
        stateMachine.Monster.Animator.SetBool(hash, false);
        
    }

}
