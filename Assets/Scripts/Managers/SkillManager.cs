using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public List<GameObject> skills;
    public GameObject[] attackSkillPrefabs;

    private void Start()
    {
        //JsonLoader를 이용해서 스킬 및 스탯 옵션 정보 받기
        DataManager.Instance.Init();
        Dictionary<int, SkillData> skillDict = DataManager.Instance.skillDict;
        foreach (var skillData in skillDict.Values)
        {
            AttackSkill skill;
            GameObject go;
            
            if (skillData.skillType == SkillType.ProjectileSkill)
                go = Instantiate(attackSkillPrefabs[0]);
               
            else if (skillData.skillType == SkillType.CircleSkill)
                go = Instantiate(attackSkillPrefabs[1]);
                
            else
                go = Instantiate(attackSkillPrefabs[2]);

            skill = go.GetComponent<AttackSkill>();
            skill.skillData = skillData;

            skill.skillName = skillData.name;
            skill.skillDescription = skillData.description;
            skill.skillLevel = 0;
            skill.damage = skillData.damage;
            skill.attackRate = skillData.attackRate;
            skill.attackRange = skillData.attackRange;
            skill.penetration = skillData.penetration;
            skill.shotSpeed = skillData.shotSpeed;
            skill.shotCount = skillData.shotCount;
            skill.isAuto = skillData.autoAttack;
            
            skills.Add(go);
            go.SetActive(false);
        }
    }
}
