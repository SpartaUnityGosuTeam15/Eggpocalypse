using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileSkill : AttackSkill
{
    Vector3 direction; //�߻� ����
    public float shotSpeed; //�߻�ӵ�
    public GameObject projectionPrefabs; //�߻�ü

    private void Update()
    {
        if (isAuto) //�ڵ� ������ �� ���� �ð����� ����
        {
            if (cooldown - Time.deltaTime < 0f)
            {
                StartCoroutine(makePrefabs());
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

        //�ӽ÷� Ÿ�� ������ ������ ���ؼ�
        direction = Vector3.zero - new Vector3(transform.position.x, 0, transform.position.z);

        GameObject go = Instantiate(projectionPrefabs);

        go.transform.position = transform.position;
        go.GetComponent<Projectile>().shotSpeed = shotSpeed;
        go.GetComponent<Projectile>().direction = direction.normalized;
        go.GetComponent<Projectile>().lifeTime = attackRange / shotSpeed;
    }

    public IEnumerator makePrefabs()
    {
        for (int i = 0; i < skillLevel; i++)
        {
            UseSkill();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
