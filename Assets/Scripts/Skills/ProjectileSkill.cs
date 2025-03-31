using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileSkill : AttackSkill
{
    Vector3 direction; //발사 방향
    public float shotSpeed; //발사속도
    public GameObject projectionPrefabs; //발사체

    private void Update()
    {
        if (isAuto) //자동 공격일 시 일정 시간마다 실행
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
        //타겟 방향으로 공격
        //direction = target.transform.position - transform.position

        //임시로 타격 방향은 원점을 향해서
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
