using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateMachine : StateMachine
{
    public Monster Monster { get; }
    public float MoveSpeed { get; set; } = 4f;
    public float RotationDamping { get; private set; } = 5f;
    public MonsterIdleState IdleState { get; }
    public MonsterAttackState AttackState { get; }
    public MonsterChasingState ChasingState { get; }
    

    //TODO: 플레이어 받아오기
    public GameObject Target { get; private set; }
    public MonsterStateMachine(Monster monster)
    {
        Monster = monster;
        IdleState = new MonsterIdleState(this);
        AttackState = new MonsterAttackState(this);
        ChasingState = new MonsterChasingState(this);

        Target = GameObject.FindGameObjectWithTag("Player");
    }
}
