using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers
{
    public class ItemManager : Singleton<ItemManager>
    {
        [SerializeField] private BulletData[] bullets;
        [SerializeField] private Item itemPrefab;

        public static Item ItemPrefab => Instance.itemPrefab;

        public static BulletData GetBullet(int index)
        {
            if (index < 0 || index >= Instance.bullets.Length)
                return null;

            return Instance.bullets[index];
        }

        public static BulletData GetRandomBullet()
        {
            int rnd = Random.Range(0, Instance.bullets.Length);
            return GetBullet(rnd);
        }
    }
}
