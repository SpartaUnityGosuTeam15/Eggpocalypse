using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileSkill : AttackSkill
{
    Vector3 direction; //�߻� ����
    
    public GameObject projectionPrefabs; //�߻�ü

    private void Start()
    {
        currentStat = SkillManager.Instance.currentStat;
        LevelUP();
    }

    private void Update()
    {
        if (GameManager.Instance.player.closest == null)
            return;

        if (isAuto) //�ڵ� ������ �� ���� �ð����� ����
        {
            if (cooldown - Time.deltaTime < 0f)
            {
                StartCoroutine(makePrefabs()); //������Ʈ Ǯ������ �ٲ� ��
                cooldown = attackRate[skillLevel] * currentStat[3];
            }
        }

        if (cooldown > 0f)
            cooldown -= Time.deltaTime;

    }

    public override void UseSkill()
    {
        direction = GameManager.Instance.player.closest.transform.position - new Vector3(transform.position.x, 0, transform.position.z);

        GameObject go = Instantiate(projectionPrefabs); //������Ʈ Ǯ������ �ٲ� ��

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
            yield return new WaitForSeconds(0.05f); //���� �߻�� �ణ�� ������
        }
    }

    public override void LevelUP()
    {
        base.LevelUP();
    }
}
