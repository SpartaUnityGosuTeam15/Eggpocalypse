using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterSkill
{
    public string SkillName {  get; private set; }
    public float Cooldown {  get; private set; }
    protected float lastUsedTime = -999f;

    public bool CanUse()
    {
        return Time.time >=lastUsedTime + Cooldown;
    }

    public void UseSkill()
    {
        if (CanUse())
        {
            lastUsedTime = Time.time;
            ExecuteSkill();
        }
    }

    protected abstract void ExecuteSkill();
}
