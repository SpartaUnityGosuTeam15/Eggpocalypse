using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSkill : BaseSkill
{
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
    public int SkillLevel
    {
        get { return skillLevel; }
        set
        {
            skillLevel = value;
        }
    }

    public int maxLevel;
    public int MaxLevel
    {
        get { return maxLevel; }
    }

    public int id;
    public float[] amount;
    internal SkillData skillData;

    //public int type; // 스탯 타입 ex) 공격력, 이속, 공속...

    private void Start()
    {
        LevelUP();
    }

    public void GetStat()
    {
        //GameManager.Instance.player -> 스탯 add 하기
    }

    public void LevelUP()
    {
        skillLevel++;
        if (skillLevel > maxLevel)
            skillLevel = maxLevel;

        SkillManager.Instance.totalStat[id][skillLevel] = amount[skillLevel];
    }
}
