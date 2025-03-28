using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSkill : BaseSkill
{
    public float amount;
    //public int type; // 스탯 타입 ex) 공격력, 이속, 공속...

    public void GetStat()
    {
        //GameManager.Instance.player -> 스탯 add 하기
    }

    public void UpdateStat()
    {
        //스탯 변동이 있을 경우
    }

    public void LevelUP()
    {
        if(maxLevel > skillLevel)
        {
            skillLevel++;
            //스킬 레벨에 따른 스탯 증가 
            //UpdateStat();
        }
    }
}
