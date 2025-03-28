using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChasingState : MonsterBaseState
{
    public MonsterChasingState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit() 
    { 
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (IsInAttackRange() && stateMachine.Monster is NamedMonster)
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }

    }

    protected bool IsInAttackRange()
    {
        if (stateMachine.Monster.Agent == null) return false;

        float remainDistance = stateMachine.Monster.Agent.remainingDistance;
        float stoppingDistance = stateMachine.Monster.Agent.stoppingDistance;
        
        return remainDistance <= stoppingDistance || remainDistance <= stateMachine.Monster.AttackRange;
    }
}
