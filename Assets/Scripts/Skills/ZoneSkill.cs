using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSkill : AttackSkill
{
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
        //몬스터 리스트에 있는 적들에게 데미지 
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
         * collision의 tag or layer가 몬스터인 경우 몬스터 리스트 추가
         */
    }

    private void OnTriggerExit(Collider other)
    {
        /*
         * collision의 tag or layer가 몬스터인 경우 몬스터 리스트 제거
         */
    }
}