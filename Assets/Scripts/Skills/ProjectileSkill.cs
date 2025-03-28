using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkill : BaseSkill
{
    Vector3 direction; //발사 방향

    private void Update()
    {
        if (isAuto) //자동 공격일 시 일정 시간마다 실행
        {
            if (cooldown - Time.deltaTime < 0f)
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
        //타겟 방향으로 공격
        //direction = target.transform.position - transform.position


    } 
}
