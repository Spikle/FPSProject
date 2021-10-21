using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponExample currentWeapon;

    private void Start()
    {
        FP_Input playerInput = GetComponent<FP_Input>();
        currentWeapon.Init(playerInput, ItemManager.GetRandomBullet());
    }

    public void SetBullet(BulletData bullet)
    {
        currentWeapon.SetBullet(bullet);
    }
}
