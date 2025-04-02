using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public static DataManager Instance { get; private set; } = new DataManager();

    public Dictionary<int, SkillData> skillDict { get; private set; } = new Dictionary<int, SkillData>();
    public Dictionary<int, MonsterData> monsterDict {  get; private set; } = new Dictionary<int, MonsterData>();
    public Dictionary<int, BuildingData> buildDict { get; private set; } = new Dictionary<int, BuildingData>();
    public Dictionary<int, StatData> statDict { get; private set; } = new Dictionary<int, StatData>();

    public void Init()
    {
        skillDict = LoadJson<SkillDataLoader, int, SkillData>(nameof(SkillData)).MakeDict();
        monsterDict = LoadJson<MonsterDataLoader, int, MonsterData>(nameof(MonsterData)).MakeDict();
        buildDict = LoadJson<BuildingDataLoader, int, BuildingData>(nameof(BuildingData)).MakeDict();
        statDict = LoadJson<StatDataLoader, int, StatData>(nameof(StatData)).MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
