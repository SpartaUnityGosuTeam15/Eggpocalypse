using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkill : BaseSkill
{
    Vector3 direction; //�߻� ����

    private void Update()
    {
        if (isAuto) //�ڵ� ������ �� ���� �ð����� ����
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
        //Ÿ�� �������� ����
        //direction = target.transform.position - transform.position


    } 
}
