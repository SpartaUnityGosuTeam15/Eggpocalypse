using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int piercing; //관통할 수 있는 횟수 //음수 설정하면 관통 횟수제한 X
    public float lifeTime; //생존시간 //음수로 생성하면 시간 X
    public Vector3 direction; //발사 방향
    public float shotSpeed;

    private void OnTriggerEnter(Collider other)
    {
        //데미지 넣는 공식
        //구현 필요


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
