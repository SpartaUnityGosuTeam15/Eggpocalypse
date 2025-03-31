using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Resource,
    Exp
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
            }
           GainItem?.Invoke();
        }
    }
}
