using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMenu : UIMenu
{
    [SerializeField] private Inputs inputs;
    [SerializeField] private Text health;
    [SerializeField] private Text ammo;

    [Header("Inventory")]
    [SerializeField] private ItemUI itemPrefab;
    [SerializeField] private Transform inventory;
    private List<ItemUI> items = new List<ItemUI>();
    
    private void Awake()
    {
        EventManager.OnUpdateWeaponAmmo += UpdateAmmo;
        EventManager.OnSetHealthPlayer += SetHealthPlayer;
        EventManager.OnUpdateInventory += UpdateInventory;
    }

    private void OnDestroy()
    {
        EventManager.OnUpdateWeaponAmmo -= UpdateAmmo;
        EventManager.OnSetHealthPlayer -= SetHealthPlayer;
        EventManager.OnUpdateInventory -= UpdateInventory;
    }

    public override void Open()
    {
        base.Open();
        EventManager.OnUpdateInputs = () => { return inputs; };
    }

    public override void Close()
    {
        base.Close();
        EventManager.OnUpdateInputs = null;
    }

    private void UpdateAmmo(int count)
    {
        ammo.text = "Ammo: " + count;
    }

    private void SetHealthPlayer(ValueStats hp)
    {
        hp.OnChange += UpdateHealth;
        UpdateHealth(hp);
    }

    private void UpdateHealth(ValueStats hp)
    {
        health.text = "Health: " + hp.Value;
    }

    private void UpdateInventory(List<ItemInventory> itemInventory)
    {
        for(int i = 0; i < itemInventory.Count; i++)
        {
            if (items.Count == i)
                items.Add(Instantiate(itemPrefab, inventory));

            items[i].Init(itemInventory[i].BulletData);
            items[i].UpdateCount(itemInventory[i].Count);
        }
    }

}
