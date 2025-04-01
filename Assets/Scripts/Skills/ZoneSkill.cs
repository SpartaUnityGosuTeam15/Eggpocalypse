using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSkill : AttackSkill
{
    //���� ����Ʈ 
    List<IDamageable> monsterList;

    private void Start()
    {
        monsterList = new List<IDamageable>();
    }


    private void Update()
    {
        if (isAuto) //�ڵ� ������ �� ���� �ð����� ����
        {
            if (cooldown - Time.deltaTime <= 0f)
            {
                UseSkill();
                cooldown = attackRate;
            }
        }

        if (cooldown > 0f)
            cooldown -= Time.deltaTime;
    }

    public override void UseSkill()
    {
        foreach (var monster in monsterList)
        {
            monster.TakeDamage((int)damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            monsterList.Add(other.GetComponent<IDamageable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            monsterList.Remove(other.GetComponent<IDamageable>());
        }
    }

    public override void LevelUP()
    {
        base.LevelUP();
    }
}