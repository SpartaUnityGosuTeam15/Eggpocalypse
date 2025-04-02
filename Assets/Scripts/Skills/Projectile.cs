using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int penetration; //������ �� �ִ� Ƚ�� //���� �����ϸ� ���� Ƚ������ X
    public float lifeTime; //�����ð�
    public bool isLifeTime; //�����ð� ����
    public Vector3 direction; //�߻� ����
    public float shotSpeed;

    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);


            if(penetration == 0)
                Destroy(gameObject);

            if (penetration > 0)
                penetration--;
        }
    }

    private void Update()
    {
        if (direction == Vector3.zero)
            return;

        transform.position += direction * shotSpeed * Time.deltaTime;

        if (!isLifeTime)
            return;

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject); //������Ʈ Ǯ������ �ٲ� ��
        }
    }
}
