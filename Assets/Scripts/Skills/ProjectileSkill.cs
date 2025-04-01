using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileSkill : AttackSkill
{
    Vector3 direction; //�߻� ����
    public float shotSpeed; //�߻�ӵ�
    public GameObject projectionPrefabs; //�߻�ü
    public int shotCount;

    private void Update()
    {
        if (GameManager.Instance.player.closest == null)
            return;

        if (isAuto) //�ڵ� ������ �� ���� �ð����� ����
        {
            if (cooldown - Time.deltaTime < 0f)
            {
                StartCoroutine(makePrefabs()); //������Ʈ Ǯ������ �ٲ� ��
                cooldown = attackRate;
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
        go.GetComponent<Projectile>().shotSpeed = shotSpeed;
        go.GetComponent<Projectile>().direction = direction.normalized;
        go.GetComponent<Projectile>().lifeTime = attackRange / shotSpeed;
    }

    public IEnumerator makePrefabs()
    {
        for (int i = 0; i < shotCount; i++)
        {
            UseSkill();
            yield return new WaitForSeconds(0.05f); //���� �߻�� �ణ�� ������
        }
    }

    public override void LevelUP()
    {
        base.LevelUP();
        //��ġ ����
    }
}
