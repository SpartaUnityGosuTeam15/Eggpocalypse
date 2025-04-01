using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Damage : Singleton<UI_Damage>
{
    private GameObject damagePrefab;
    protected override void Awake()
    {
        damagePrefab = Resources.Load<GameObject>("Prefabs/DamageText");

    }

    public void DamagePool(int damage, Vector3 position)
    {
        Poolable damagePool = PoolManager.Instance.Get(damagePrefab);


        Damage damageText = damagePool.GetComponent<Damage>();

        damageText.ShowDamage(damage, position, damagePool);

    }
}
