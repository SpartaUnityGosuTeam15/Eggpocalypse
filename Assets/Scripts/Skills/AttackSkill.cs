using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class AttackSkill : BaseSkill
{
    public float[] damage;        //������
    public float[] attackRate;    //���� �ֱ�
    public float[] shotSpeed;     //�߻�ӵ�
    public float[] attackRange;   //���� ��Ÿ�
    public int[] penetration; //�����ϴ� ����
    public int[] shotCount;       //�߻� Ƚ��
    public bool isAuto;         //�ڵ� ���

    public float cooldown;      //���� ��Ÿ��

    public abstract void UseSkill();
}
