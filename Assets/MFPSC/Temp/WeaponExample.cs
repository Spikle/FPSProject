using UnityEngine;
using System.Collections;
using Scripts.Managers;

/// <summary>
/// This is not a functional weapon script. It just shows how to implement shooting and reloading with buttons system.
/// </summary>
public class WeaponExample : MonoBehaviour 
{
    [SerializeField] private FP_Input playerInput;

    [SerializeField] private float shootRate = 0.15F;
    [SerializeField] private float reloadTime = 1.0F;
    [SerializeField] private int ammoCount = 15;

    [SerializeField] private float forceBullet = 5000;
    [SerializeField] private float damage = 1;

    private int ammo;
    private float delay;
    private bool reloading;

    private BulletData bullet;

	void Start () 
    {
        ammo = ammoCount;
	}

    public void Init(FP_Input playerInput, BulletData bullet)
    {
        this.playerInput = playerInput;
        this.bullet = bullet;
        EventManager.OnUpdateWeaponAmmo?.Invoke(ammoCount);
    }

    public void SetBullet(BulletData bullet)
    {
        this.bullet = bullet;
    }

	
	void Update () 
    {
        if(playerInput.Shoot())                         //IF SHOOT BUTTON IS PRESSED (Replace your mouse input)
            if(Time.time > delay)
                Shoot();

        if (playerInput.Reload())                        //IF RELOAD BUTTON WAS PRESSED (Replace your keyboard input)
            if (!reloading && ammoCount < ammo)
                StartCoroutine("Reload");

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red);
	}

    void Shoot()
    {
        if (ammoCount > 0)
        {
            Debug.Log("Shoot");
            ammoCount--;
            EventManager.OnUpdateWeaponAmmo?.Invoke(ammoCount);
            Bullet bulletGO = PoolManager.Spawn(bullet.Prefab.name, transform.position, Quaternion.identity) as Bullet;
            bulletGO.Shoot(Camera.main.transform.forward, forceBullet, damage, playerInput.transform);
        }
        else
            Debug.Log("Empty");

        delay = Time.time + shootRate;
    }

    IEnumerator Reload()
    {
        reloading = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadTime);
        ammoCount = ammo;
        Debug.Log("Reloading Complete");
        EventManager.OnUpdateWeaponAmmo?.Invoke(ammoCount);
        reloading = false;
    }
}
