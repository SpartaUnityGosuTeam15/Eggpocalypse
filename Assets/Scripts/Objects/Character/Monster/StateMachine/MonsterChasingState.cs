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
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Monster.AttackRange;
    }
}
