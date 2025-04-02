using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileSkill : AttackSkill
{
    Vector3 direction; //발사 방향
    
    public GameObject projectionPrefabs; //발사체

    private void Start()
    {
        currentStat = SkillManager.Instance.currentStat;
        LevelUP();
    }

    private void Update()
    {
        if (GameManager.Instance.player.closest == null)
            return;

        if (isAuto) //자동 공격일 시 일정 시간마다 실행
        {
            if (cooldown - Time.deltaTime < 0f)
            {
                StartCoroutine(makePrefabs()); //오브젝트 풀링으로 바꿀 것
                cooldown = attackRate[skillLevel] * currentStat[3];
            }
        }

        if (cooldown > 0f)
            cooldown -= Time.deltaTime;

    }

    public override void UseSkill()
    {
        direction = GameManager.Instance.player.closest.transform.position - new Vector3(transform.position.x, 0, transform.position.z);

        GameObject go = Instantiate(projectionPrefabs); //오브젝트 풀링으로 바꿀 것

        go.transform.position = transform.position;
        go.GetComponent<Projectile>().shotSpeed = shotSpeed[skillLevel];
        go.GetComponent<Projectile>().direction = direction.normalized;
        go.GetComponent<Projectile>().lifeTime = (attackRange[skillLevel] + currentStat[5]) / shotSpeed[skillLevel];
        go.GetComponent<Projectile>().damage = (int)(damage[skillLevel] + currentStat[2]);
        go.GetComponent<Projectile>().penetration = penetration[skillLevel];
        go.GetComponent<Projectile>().isLifeTime = true;
    }

    public IEnumerator makePrefabs()
    {
        for (int i = 0; i < shotCount[skillLevel] + (int)currentStat[4]; i++)
        {
            UseSkill();
            yield return new WaitForSeconds(0.05f); //연속 발사시 약간의 딜레이
        }
    }

    public override void LevelUP()
    {
        base.LevelUP();
    }
}
