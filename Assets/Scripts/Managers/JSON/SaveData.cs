//캐릭터 정보 세이브 로드 용도
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

    public int GetAttack()
    {
        return enchantState[0] * 1;
    }
    public int GetHealth()
    {
        return enchantState[1] * 10;
    }

    public float GetMoveSpeed()
    {
        return enchantState[2] * 1;
    }

    public float GetAttackSpeed()
    {
        return 1 - (enchantState[3] * 0.1f);
    }

    public int GetAttackRange()
    {
        return enchantState[4] * 1;
    }

    public int GetProjectileCount()
    {
        return enchantState[5] * 1;
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
