using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSkill : BaseSkill
{
    public float amount;
    //public int type; // ���� Ÿ�� ex) ���ݷ�, �̼�, ����...

    public void GetStat()
    {
        //GameManager.Instance.player -> ���� add �ϱ�
    }

    public void UpdateStat()
    {
        //���� ������ ���� ���
    }

    public void LevelUP()
    {
        if(maxLevel > skillLevel)
        {
            skillLevel++;
            //��ų ������ ���� ���� ���� 
            //UpdateStat();
        }
    }
}
