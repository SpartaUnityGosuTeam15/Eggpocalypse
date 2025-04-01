using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public SkillData skillData;

    public string skillName;
    public string skillDescription;
    public int skillLevel;
    public int maxLevel = 6;

    public virtual void LevelUP()
    {
        skillLevel++;
        if(skillLevel > maxLevel)
            skillLevel = maxLevel;
    }
}
