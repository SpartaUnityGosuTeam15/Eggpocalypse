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
    public int ID { 
        get { return id; }
        set
        {
            id = value;
        }
    }
    public float[] amount;

    public void LevelUP()
    {
        if (id < 0 || id >= 6)
            return;

        if (skillLevel == 6)
            return;

        skillLevel++;
        
        SkillManager.Instance.UpdateStat(id);
    }
}
