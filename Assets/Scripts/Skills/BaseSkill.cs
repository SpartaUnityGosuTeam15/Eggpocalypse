using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseSkill : MonoBehaviour
{
    public string skillName;
    public int skillLevel;
    public const int maxLevel = 6;

    public float damage;        //데미지
    public float attackRate;    //공격 주기

    public SkillType skillType;

    //public List<Monster> targets; //몬스터 리스트는 플레이어한테서 참조 정도만 
}

public enum SkillType
{
    AreaEffect, //장판
    Targeting, //특정 상대를 항한 공격
    NonTargeting // 일정 방향을 향한 공격 //ex) 뱀서 채찍
}
