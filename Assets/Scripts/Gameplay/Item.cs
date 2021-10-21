using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPoolObject
{
    private BulletData bulletData;

    public BulletData BulletData => bulletData;

    private void Start()
    {
        bulletData = ItemManager.GetRandomBullet();
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.sharedMaterial = bulletData.Prefab.GetComponent<MeshRenderer>().sharedMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Inventory>(out Inventory inv))
        {
            ItemInventory i = new ItemInventory(BulletData, 1);
            inv.AddItem(i);
            ReturnToPool();
        }
    }

    public void Spawn(Vector3 position, Quaternion quaternion)
    {
        transform.position = position;
        transform.rotation = quaternion;
        gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        transform.position = Vector3.zero;
    }

}
