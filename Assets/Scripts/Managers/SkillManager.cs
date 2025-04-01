using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public GameObject[] attackSkillPrefabs;
    Dictionary<int, SkillData> skillDict = DataManager.Instance.skillDict;

    private void Start()
    {
        DataManager.Instance.Init();
    }

    public AttackSkill GetSkill(int id, Transform trans)
    {
        GameObject go;
        SkillData skillData;
        if(!skillDict.TryGetValue(id, out skillData))
        {
            return null; //id 값이 없는 경우
        }

        if (skillData.skillType == SkillType.ProjectileSkill)
            go = Instantiate(attackSkillPrefabs[0], trans);

        else if (skillData.skillType == SkillType.CircleSkill)
            go = Instantiate(attackSkillPrefabs[1], trans);

        else
            go = Instantiate(attackSkillPrefabs[2], trans);

        AttackSkill skill = go.GetComponent<AttackSkill>();
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

        return skill;
    }
}
