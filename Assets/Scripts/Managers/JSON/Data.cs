// 캐릭터, 아이템 등의 초기값 로드 용도
using System;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

#region SkillData

public enum SkillType
{
    ProjectileSkill,
    CircleSkill,
    ZoneSkill
}

[Serializable]
public class SkillData
{
    public int id;
    public string name;
    public string description;      // 설명
    public float[] damage = new float[6];  //데미지
    public float[] attackRate = new float[6]; //공격속도
    public float[] shotSpeed = new float[6]; //발사체 속도
    public float[] attackRange = new float[6]; //사거리
    public int[] penetration = new int[6]; //관통하는 몹수
    public int[] shotCount = new int[6]; //한 번 발사시 횟수
    public bool autoAttack = true; //자동 공격 여부
    public SkillType skillType;
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
    public int dropExp;
    public int dropMeat;
    public int dropGold;
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

