using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSkill : AttackSkill
{
    public LayerMask monsterLayer;

    public override void Start()
    {
        base.Start();
        currentStat = SkillManager.Instance.currentStat;
        LevelUP();
    }


    private void Update()
    {
        if (isAuto) //자동 공격일 시 일정 시간마다 실행
        {
            if (cooldown - Time.deltaTime <= 0f)
            {
                UseSkill();
                cooldown = attackRate[skillLevel] * currentStat[3] * addictionAttackRate;
            }
        }

        if (cooldown > 0f)
            cooldown -= Time.deltaTime;
    }

    public override void UseSkill()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange[skillLevel] / 2, monsterLayer);

        foreach (var hit in hits)
        {
            hit.GetComponent<IDamageable>().TakeDamage((int)(damage[skillLevel] + currentStat[2] + addictionAttack));
        }
    }

    public override void LevelUP()
    {
        base.LevelUP();
        transform.localScale = new Vector3(attackRange[skillLevel], 0.05f, attackRange[skillLevel]);
    }
}