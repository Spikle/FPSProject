using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : HealthController
{
    public override void Dead()
    {
        PoolManager.Spawn(ItemManager.ItemPrefab.name, transform.position, Quaternion.identity);
        OnDead?.Invoke();
    }
}
