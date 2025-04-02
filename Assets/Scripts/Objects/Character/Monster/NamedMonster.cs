using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedMonster : Monster
{
    private List<MonsterSkill> skills = new();
    [field: SerializeField] public MonsterAnimationData AnimationData { get; private set; }
    public override void Awake()
    {
        base.Awake();
        AnimationData.Initialize();
        //InitSkills();
    }

    private void InitSkills()
    {
        skills.AddRange(GetComponents<MonsterSkill>());
    }
    public void UseRandomSkill()
    {
        if (skills.Count == 0) return;
        MonsterSkill skill = skills[Random.Range(0, skills.Count)]; 
        skill.UseSkill();
    }
}
