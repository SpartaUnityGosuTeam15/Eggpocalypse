//ĳ���� ���� ���̺� �ε� �뵵
using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int eggIndex;
    public int stageIndex;
    public int gold;
    public List<int> enchantState;

    public SaveData()
    {
        eggIndex = 0;
        stageIndex = 0;
        gold = 1000;
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
