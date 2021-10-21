using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Managers;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] private List<PoolPart> pools = new List<PoolPart>();
    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }


    public void Initialize()
    {
        foreach(var pool in pools)
            for (int i = 0; i < pool.Count; i++)
            {
                IPoolObject poolObject = Instantiate(pool.Prefab, transform).GetComponent<IPoolObject>();
                poolObject.ReturnToPool();
                pool.polling.Add(poolObject);
            }
    }

    public static IPoolObject Spawn(string name, Vector3 position, Quaternion rotation)
    {
        var pool = Instance.pools.Find(x=> string.Compare(x.Name, name) == 0);

        if (pool == null)
        {
            Debug.LogError("Object in pool not found");
            return null;
        }

        IPoolObject poolObject = GetObject(pool);
        poolObject.Spawn(position, rotation);
        return poolObject;
    }

    private static IPoolObject GetObject(PoolPart pool)
    {
        foreach (var obj in pool.polling)
        {
            if (!obj.gameObject.activeSelf)
                return obj;
        }

        IPoolObject poolObject = Instantiate(pool.Prefab, Instance.transform).GetComponent<IPoolObject>();
        pool.polling.Add(poolObject);
        return poolObject;
    }
}

[System.Serializable]
public class PoolPart
{
	public string Name;
	public GameObject Prefab;
	public int Count;
    public List<IPoolObject> polling = new List<IPoolObject>();
}
