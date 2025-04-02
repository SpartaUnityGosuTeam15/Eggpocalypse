using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterBaseState
{
    bool alreadyAppliedDealing;
    public MonsterAttackState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (stateMachine.Monster is NamedMonster monster)
            StartAnimation(monster.AnimationData.AttackHash);

        alreadyAppliedDealing = false;
    }
    public override void Exit()
    {
        base.Exit();
        if (stateMachine.Monster is NamedMonster monster)
            StopAnimation(monster.AnimationData.AttackHash);
    }
    public override void Update()
    {
        base.Update();
        if (!alreadyAppliedDealing)
        {
            //Attack();
            alreadyAppliedDealing = false;
        }

        if (!stateMachine.ChasingState.IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
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
