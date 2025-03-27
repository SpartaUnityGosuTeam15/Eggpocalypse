using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<int, SkillData> skillDict { get; private set; } = new Dictionary<int, SkillData>();
    public Dictionary<int, MonsterData> monsterDict {  get; private set; } = new Dictionary<int, MonsterData>();

    public void Init()
    {
        skillDict = LoadJson<SkillDataLoader, int, SkillData>(nameof(SkillData)).MakeDict();
        monsterDict = LoadJson<MonsterDataLoader, int, MonsterData>(nameof(MonsterData)).MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
