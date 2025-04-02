using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSkillData
{
    public int id;
    public string name;
    public int level;
    public int type; //0이면 attack // 1면 stat
    //혹시나 추가 정보 필요하면 추가

    public ChoiceSkillData(int id, string name, int level, int type)
    {
        this.id = id;
        this.name = name;
        this.level = level;
        this.type = type;
    }
}

public class SkillManager : Singleton<SkillManager>
{
    public GameObject[] attackSkillPrefabs;

    Dictionary<int, SkillData> skillDict; //Attack Skill
    
    Dictionary<int, StatData> statDict; //Stat Skill

    public Action updateDamage;

    public List<StatSkill> statSkillList;

    public float[][] totalStat;  //쓸 때 -> totalStat[id][level - 1];
    public int[] statLevel = new int[6]; //가진 스탯의 레벨
    public float[] currentStat;

    public Dictionary<int, ChoiceSkillData> allSkillDict; //스킬 선택을 위한 dict

    private void Start()
    {
        totalStat = new float[6][];
        for(int i = 0; i < 6; i++)
            totalStat[i] = new float[7];

        skillDict = DataManager.Instance.skillDict;
        statDict = DataManager.Instance.statDict;

        totalStat[0] = statDict[0].health;
        totalStat[1] = statDict[1].moveSpeed;
        totalStat[2] = statDict[2].attack;
        totalStat[3] = statDict[3].attackSpeed;
        totalStat[4] = statDict[4].projectileIncrement;
        totalStat[5] = statDict[5].range;

        currentStat = new float[7];
        InitStat();

        
        statSkillList = new List<StatSkill>();
        for(int i = 0; i < 6;i++)
        {
            StatSkill item = new StatSkill();
            item.id = i;
            item.amount = totalStat[i];
            item.skillLevel = 0;
            item.maxLevel = 6;
            item.SkillName = statDict[i].name;
            statSkillList.Add(item);
        }
        
    }

    public void InitStat() //스탯 정보 업데이트 //스킬 리스트 업데이트
    {

        for (int i = 0; i < 6; i++)
        {
            currentStat[i] = totalStat[i][statLevel[i]];
        }

        allSkillDict = new Dictionary<int, ChoiceSkillData>();
        foreach (var data in skillDict)
        {
            allSkillDict.Add(data.Key, new ChoiceSkillData(data.Key, data.Value.name, 0, 0));
        }
        foreach (var data in statDict)
        {
            allSkillDict.Add(data.Key, new ChoiceSkillData(data.Key, data.Value.name, 0, 1));
        }
    }
    
    public void UpdateStat(int id) //스탯 정보 업데이트
    {
        if (statLevel[id] >= 6) return;

        statLevel[id]++;

        currentStat[id] = totalStat[id][statLevel[id]];
        updateDamage?.Invoke();
    }

    public AttackSkill GetSkill(int id, Transform trans)
    {
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
        skill.id = skillData.id;
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
