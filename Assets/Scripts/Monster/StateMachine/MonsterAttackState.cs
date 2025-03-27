using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterBaseState
{
    bool alreadyApplyForce;
    bool alreadyAppliedDealing;
    public MonsterAttackState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        alreadyApplyForce = false;
        alreadyAppliedDealing = false;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (IsInChasingRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
        else
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
}
