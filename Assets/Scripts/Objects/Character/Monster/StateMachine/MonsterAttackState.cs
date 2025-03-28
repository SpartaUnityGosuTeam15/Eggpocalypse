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
        if (!alreadyAppliedDealing)
        {
            Attack();
            alreadyAppliedDealing = false;
        }
    }

    private void Attack()
    {
        if(stateMachine.Monster is NamedMonster namedMonster)
        {
            namedMonster.UseRandomSkill();
            alreadyAppliedDealing = true;
            return;
        }
    }
}
