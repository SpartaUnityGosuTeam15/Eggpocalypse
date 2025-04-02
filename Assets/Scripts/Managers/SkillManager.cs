using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public GameObject[] attackSkillPrefabs;

    Dictionary<int, SkillData> skillDict;

    Dictionary<int, StatData> statDict;

    public float[][] totalStat;  //쓸 때 -> totalStat[id][level - 1];
    public int[] statLevel = new int[6]; //가진 스탯의 레벨

    private void Start()
    {
        totalStat = new float[6][];
        for(int i = 0; i < 6; i++)
            totalStat[i] = new float[7];

        DataManager.Instance.Init();
        skillDict = DataManager.Instance.skillDict;
        statDict = DataManager.Instance.statDict;

        totalStat[0] = statDict[0].health;
        totalStat[1] = statDict[1].moveSpeed;
        totalStat[2] = statDict[2].attack;
        totalStat[3] = statDict[3].attackSpeed;
        totalStat[4] = statDict[4].projectileIncrement;
        totalStat[5] = statDict[5].range;

    }

    public float[] GetStat(int id) //스킬 사용시 정보 가져오기
    {
        float[] stat = new float[7];

        for(int i = 0; i < 6; i++)
        {
            stat[i] = totalStat[i][statLevel[i]];
        }


        return totalStat[id];
    }

    public AttackSkill GetSkill(int id, Transform trans)
    {
        //Dictionary<int, SkillData> skillDict = DataManager.Instance.skillDict;
        GameObject go;
        SkillData skillData;
        if(!skillDict.TryGetValue(id, out skillData))
        {
            Debug.Log("잘못된 id");
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
