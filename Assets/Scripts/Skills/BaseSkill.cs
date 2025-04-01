using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public SkillData skillData;

    public string skillName;
    public int skillLevel;
    public readonly int maxLevel = 6;
}
