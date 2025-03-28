using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseSkill : Options
{
    public float damage;        //������
    public float attackRate;    //���� �ֱ�
    public float attackRange;   //���� ��Ÿ�

    public float cooldown;      //���� ��Ÿ��

    public bool isAuto;         //�ڵ� ���

    public abstract void UseSkill();
}
