using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseSkill : MonoBehaviour
{
    public string skillName;
    public int skillLevel;
    public const int maxLevel = 6;

    public float damage;        //������
    public float attackRate;    //���� �ֱ�

    public SkillType skillType;

    //public List<Monster> targets; //���� ����Ʈ�� �÷��̾����׼� ���� ������ 
}

public enum SkillType
{
    AreaEffect, //����
    Targeting, //Ư�� ��븦 ���� ����
    NonTargeting // ���� ������ ���� ���� //ex) �켭 ä��
}
