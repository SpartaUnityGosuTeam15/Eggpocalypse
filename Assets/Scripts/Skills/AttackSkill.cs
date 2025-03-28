using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class AttackSkill : BaseSkill
{
    public float damage;        //데미지
    public float attackRate;    //공격 주기
    public float attackRange;   //공격 사거리

    public float cooldown;      //공격 쿨타운

    public bool isAuto;         //자동 사용

    public abstract void UseSkill();
}
