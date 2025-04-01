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
        LevelUP();
    }


    private void Update()
    {
        if (isAuto) //�ڵ� ������ �� ���� �ð����� ����
        {
            if (cooldown - Time.deltaTime <= 0f)
            {
                UseSkill();
                cooldown = attackRate[skillLevel - 1];
            }
        }

        if (cooldown > 0f)
            cooldown -= Time.deltaTime;
    }

    public override void UseSkill()
    {
        foreach (var monster in monsterList)
        {
            monster.TakeDamage((int)damage[skillLevel - 1]);
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
        transform.localScale = new Vector3(attackRange[skillLevel - 1], 0.05f, attackRange[skillLevel - 1]);
    }
}