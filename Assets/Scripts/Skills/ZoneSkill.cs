using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSkill : AttackSkill
{
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
        //���� ����Ʈ�� �ִ� ���鿡�� ������ 
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
         * collision�� tag or layer�� ������ ��� ���� ����Ʈ �߰�
         */
    }

    private void OnTriggerExit(Collider other)
    {
        /*
         * collision�� tag or layer�� ������ ��� ���� ����Ʈ ����
         */
    }
}