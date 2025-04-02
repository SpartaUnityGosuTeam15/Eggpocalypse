// 캐릭터, 아이템 등의 초기값 로드 용도
using System;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}
public enum BuildingType
{
    Egg,
    Dragon
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
    public float[] damage = new float[7];  //데미지
    public float[] attackRate = new float[7]; //공격속도
    public float[] shotSpeed = new float[7]; //발사체 속도
    public float[] attackRange = new float[7]; //사거리
    public int[] penetration = new int[7]; //관통하는 몹수
    public int[] shotCount = new int[7]; //한 번 발사시 횟수
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
    public int health;
    public BuildingType type;
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

#region StatData

[Serializable]
public class StatData
{
    public int id;
    public string name;
    public float[] health = new float[7];
    public float[] moveSpeed = new float[7];
    public float[] attack = new float[7];
    public float[] attackSpeed = new float[7];
    public float[] projectileIncrement = new float[7];
    public float[] range = new float[7];
}

[Serializable]
public class StatDataLoader : ILoader<int, StatData>
{
    public List<StatData> data = new List<StatData>();

    public Dictionary<int, StatData> MakeDict()
    {
        Dictionary<int, StatData> dict = new Dictionary<int, StatData>();
        foreach (StatData stat in data)
        {
            dict.Add(stat.id, stat);
        }
        return dict;
    }
}

#endregion

#region StageData

[Serializable]
public class StageData
{
    public int id;
    public string name;
    public string description;
}

[Serializable]
public class StageDataLoader : ILoader<int, StageData>
{
    public List<StageData> data = new List<StageData>();

    public Dictionary<int, StageData> MakeDict()
    {
        Dictionary<int, StageData> dict = new Dictionary<int, StageData>();
        foreach (StageData stage in data)
        {
            dict.Add(stage.id, stage);
        }
        return dict;
    }
}

#endregion

