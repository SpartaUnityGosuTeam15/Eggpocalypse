// 캐릭터, 아이템 등의 초기값 로드 용도
using System;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

#region SkillData

[Serializable]
public class SkillData
{
    public int id;
    public string name;
    public string description;      // 설명
    public float coefficient;       // 계수
    public bool isPenetrating;      // 관통 여부
    public int targetCount;         
    public float range;             
    public int projectileCount;     
    public float cooldown;          
}

[Serializable]
public class SkillDataLoader : ILoader<int, SkillData>
{
    public List<SkillData> data = new List<SkillData>();

    public Dictionary<int, SkillData> MakeDict()
    {
        Dictionary<int, SkillData> dict = new Dictionary<int, SkillData>();
        foreach (SkillData skill in data)
        {
            dict.Add(skill.id, skill);
        }
        return dict;
    }
}

#endregion

#region MonsterData

[Serializable]
public class MonsterData
{
    public int id;
    public string name;
    public string description;
    public int health;
    public int attack;
    public float moveSpeed;
}

[Serializable]
public class MonsterDataLoader : ILoader<int, MonsterData>
{
    public List<MonsterData> data = new List<MonsterData>();

    public Dictionary<int, MonsterData> MakeDict()
    {
        Dictionary<int, MonsterData> dict = new Dictionary<int, MonsterData>();
        foreach (MonsterData monster in data)
        {
            dict.Add(monster.id, monster);
        }
        return dict;
    }
}

#endregion

#region BuildingData

[Serializable]
public class BuildingData
{
    public int id;
    public int skillId;
    public int attack;
    public int level;
    public int maxLevel;
}

[Serializable]
public class BuildingDataLoader : ILoader<int, BuildingData>
{
    public List<BuildingData> data = new List<BuildingData>();

    public Dictionary<int, BuildingData> MakeDict()
    {
        Dictionary<int, BuildingData> dict = new Dictionary<int, BuildingData>();
        foreach (BuildingData building in data)
        {
            dict.Add(building.id, building);
        }
        return dict;
    }
}

#endregion