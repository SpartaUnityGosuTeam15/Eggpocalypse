using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterAnimationData 
{
    [SerializeField] private string attackName = "@Attack";
    [SerializeField] private string baseAttackName = "BaseAttack";

    [SerializeField] private string groundName = "@Ground";
    [SerializeField] private string chaseName = "Chase";

    public int GroundHash {  get; private set; }
    public int ChaseHash { get; private set; }
    public int AttackHash {  get; private set; }
    public int BaseAttackHash { get; private set;}

    public void Initialize()
    {
        GroundHash = Animator.StringToHash(groundName);
        ChaseHash = Animator.StringToHash(chaseName);
        AttackHash = Animator.StringToHash(attackName);
        BaseAttackHash = Animator.StringToHash(baseAttackName);
    }
}
