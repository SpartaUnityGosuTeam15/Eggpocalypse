using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterBaseState
{
    public MonsterAttackState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (stateMachine.Monster is NamedMonster monster)
            StartAnimation(monster.AnimationData.AttackHash);
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
        if (!stateMachine.ChasingState.IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }
    }
}
