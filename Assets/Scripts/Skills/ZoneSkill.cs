using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSkill : AttackSkill
{
    //몬스터 리스트 
    List<IDamageable> monsterList;

    private void Start()
    {
        monsterList = new List<IDamageable>();
    }


    private void Update()
    {
        if (isAuto) //자동 공격일 시 일정 시간마다 실행
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