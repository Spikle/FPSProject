using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Bullet : MonoBehaviour, IPoolObject
{
    private Rigidbody rb;
    private Transform owner;
    private float damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 direction, float force, float damage, Transform owner)
    {
        rb.AddForce(direction * force);
        this.damage = damage;
        this.owner = owner;
        Observable.Timer(System.TimeSpan.FromSeconds(5)).TakeUntilDestroy(this).Subscribe(_ => ReturnToPool());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == owner)
            return;

        if(collision.transform.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemy))
        {
            enemy.OnDamage(damage);
        }

        Observable.Timer(System.TimeSpan.FromSeconds(0.5f)).TakeUntilDestroy(this).Subscribe(_ => ReturnToPool());
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
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;
    }
}
