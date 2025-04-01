// ĳ����, ������ ���� �ʱⰪ �ε� �뵵
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
    public string description;      // ����
    public float[] damage = new float[6];  //������
    public float[] attackRate = new float[6]; //���ݼӵ�
    public float[] shotSpeed = new float[6]; //�߻�ü �ӵ�
    public float[] attackRange = new float[6]; //��Ÿ�
    public int[] penetration = new int[6]; //�����ϴ� ����
    public int[] shotCount = new int[6]; //�� �� �߻�� Ƚ��
    public bool autoAttack = true; //�ڵ� ���� ����
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

