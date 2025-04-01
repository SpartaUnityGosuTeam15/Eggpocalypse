using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int penetration; //관통할 수 있는 횟수 //음수 설정하면 관통 횟수제한 X
    public float lifeTime; //생존시간
    public bool isLifeTime; //생존시간 여부
    public Vector3 direction; //발사 방향
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
            Destroy(gameObject); //오브젝트 풀링으로 바꿀 것
        }
    }
}
