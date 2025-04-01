using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Resource,
    Exp,
    Gold
}
public class Item : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private ItemType itemType;

    public Action GainItem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (itemType)
            {
                case ItemType.Resource:
                    GameManager.Instance.player.condition.GainMeat(value);
                    break;
                case ItemType.Exp:
                    GameManager.Instance.player.condition.GainExp(value);
                    break;
                case ItemType.Gold:
                    //GameManager.Instance.player.condition.GainGold(value);
                    break;
                default:
                    break;
            }
           GainItem?.Invoke();
        }
    }
}
