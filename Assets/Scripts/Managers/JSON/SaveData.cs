//캐릭터 정보 세이브 로드 용도
using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int id;
    public int eggIndex;
    public int stageIndex;
    public int gold;
    public List<int> enchantState;

    public SaveData()
    {
        id = 0;
        eggIndex = 0;
        stageIndex = 0;
        gold = 0;
        enchantState = new List<int> { 0, 0, 0, 0, 0, 0 };
    }
}

//[Serializable]
//public class SaveDataLoader
//{
//    public SaveData data;

//    public SaveDataLoader()
//    {
//        data = new SaveData();
//    }
//}
