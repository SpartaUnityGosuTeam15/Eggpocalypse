using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Damage : MonoBehaviour
{
    TextMeshPro damageTxt;
    private void Awake()
    {
        damageTxt = GetComponent<TextMeshPro>();
    }
    public void ShowDamage(int damage, Vector3 position, Poolable pool)
    {
        damageTxt.text = damage.ToString();
        transform.position = position;
        transform.DOMoveY(transform.position.y + 1f, 1f).SetEase(Ease.OutQuart).OnComplete(() => 
        {
            PoolManager.Instance.Release(pool);
        });
    }
}
