using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedMonster : Monster
{

    [field: SerializeField] public MonsterAnimationData AnimationData { get; private set; }
    public override void Awake()
    {
        base.Awake();
        AnimationData.Initialize();

    }

}
