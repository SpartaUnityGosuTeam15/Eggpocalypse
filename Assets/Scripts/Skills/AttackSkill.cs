using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class AttackSkill : MonoBehaviour, BaseSkill 
{
    public SkillData skillData;

    public string skillName;
    public string SkillName
    {
        get { return skillName; }
        set
        {
            skillName = value;
        }
    }

    public string skillDescription;
    public string SkillDescription
    {
        get { return skillDescription; }
        set
        {
            skillDescription = value;
        }
    }

    public int skillLevel;
    public int SkillLevel {
        get { return skillLevel; }
        set
        {
            skillLevel = value;
        }
    }

    public int maxLevel = 6;
    public int MaxLevel {
        get { return maxLevel; }
    }

    public int id;
    public int ID
    {
        get { return id; }
        set
        {
            id = value;
        }
    }
    public float[] damage;        //데미지
    public float[] attackRate;    //공격 주기
    public float[] shotSpeed;     //발사속도
    public float[] attackRange;   //공격 사거리
    public int[] penetration; //관통하는 몹수
    public int[] shotCount;       //발사 횟수
    public bool isAuto;         //자동 사용

    public float cooldown;      //공격 쿨타운

    public float[] currentStat;

    public int addictionShotCount;
    public float addictionAttackRate;
    public float addictionRange;
    public int addictionAttack;

    public virtual void Start()
    {
        addictionShotCount = SaveManager.Instance.saveData.GetProjectileCount();
        addictionRange = SaveManager.Instance.saveData.GetAttackRange();
        addictionAttackRate = SaveManager.Instance.saveData.GetAttackSpeed();
        addictionAttack = GameManager.Instance.player.gameObject.GetComponent<PlayerCondition>().attack;
    }

    public abstract void UseSkill();
    public virtual void LevelUP()
    {
        skillLevel++;
        if (skillLevel > maxLevel)
            skillLevel = maxLevel;
    }
}
