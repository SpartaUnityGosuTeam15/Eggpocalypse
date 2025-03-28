using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateMachine : StateMachine
{
    public Monster Monster { get; }
    public float MoveSpeed { get; set; } = 4f;
    public float RotationDamping { get; private set; } = 5f;
    public MonsterAttackState AttackState { get; }
    public MonsterChasingState ChasingState { get; }
    
    public Player Target { get; private set; }
    public MonsterStateMachine(Monster monster)
    {
        Monster = monster;
        AttackState = new MonsterAttackState(this);
        ChasingState = new MonsterChasingState(this);

        Target = GameManager.Instance.player;
    }
}
