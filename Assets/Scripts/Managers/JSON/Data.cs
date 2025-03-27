//캐릭터, 아이템 등의 초기값 로드 용도
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
}

[Serializable]
public class SkillDataLoader : ILoader<int, SkillData>
{
    public List<SkillData> data = new List<SkillData>();

    public Dictionary<int, SkillData> MakeDict()
    {
        Dictionary<int, SkillData> dict = new Dictionary<int, SkillData>();
        foreach(SkillData skill in data)
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