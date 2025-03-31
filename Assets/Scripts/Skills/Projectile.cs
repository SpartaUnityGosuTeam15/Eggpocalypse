using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int piercing; //������ �� �ִ� Ƚ�� //���� �����ϸ� ���� Ƚ������ X
    public float lifeTime; //�����ð� //������ �����ϸ� �ð� X
    public Vector3 direction; //�߻� ����
    public float shotSpeed;

    private void OnTriggerEnter(Collider other)
    {
        //������ �ִ� ����
        //���� �ʿ�


        //if(piercing == 0)
            //Destroy(gameObject);

        if(piercing > 0) 
            piercing--;
    }

    private void Update()
    {
        if (direction == Vector3.zero)
            return;

        transform.position += direction * shotSpeed * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
