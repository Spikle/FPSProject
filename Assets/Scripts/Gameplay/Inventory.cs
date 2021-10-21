using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Managers;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemInventory> itemInventories = new List<ItemInventory>();

    private WeaponController weaponController;

    private void Awake()
    {
        weaponController = GetComponent<WeaponController>();
    }

    public void AddItem(ItemInventory item)
    {
        var i = itemInventories.Find(x => x.BulletData.Equals(item.BulletData));
        if (i != null)
        {
            i.Count++;
        }
        else
            itemInventories.Add(item);

        EventManager.OnUpdateInventory?.Invoke(itemInventories);

        weaponController.SetBullet(item.BulletData);
    }
}
