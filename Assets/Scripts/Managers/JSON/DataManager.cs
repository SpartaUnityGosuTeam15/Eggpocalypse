using JetBrains.Annotations;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public Dictionary<int, SkillData> skillDict { get; private set; } = new();
    public Dictionary<int, MonsterData> monsterDict {  get; private set; } = new();
    public Dictionary<int, BuildingData> buildDict { get; private set; } = new();
    public Dictionary<int, StageData> stageDict { get; private set; } = new();
    public Dictionary<int, StatData> statDict { get; private set; } = new();

    protected override void Awake()
    {
        base.Awake();

        skillDict = LoadJson<SkillDataLoader, int, SkillData>(nameof(SkillData)).MakeDict();
        monsterDict = LoadJson<MonsterDataLoader, int, MonsterData>(nameof(MonsterData)).MakeDict();
        buildDict = LoadJson<BuildingDataLoader, int, BuildingData>(nameof(BuildingData)).MakeDict();
        stageDict = LoadJson<StageDataLoader, int, StageData>(nameof(StageData)).MakeDict();
        statDict = LoadJson<StatDataLoader, int, StatData>(nameof(StatData)).MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
