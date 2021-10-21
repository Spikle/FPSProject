using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInventory
{
    [SerializeField] private BulletData bulletData;
    [SerializeField] private int count;

    public BulletData BulletData => bulletData;
    public int Count { get { return count; } set { count = value; } }

    public ItemInventory(BulletData bulletData, int count)
    {
        this.bulletData = bulletData;
        this.count = count;
    }
}
