using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolObject
{
    public GameObject gameObject { get; }
    public void Spawn(Vector3 position, Quaternion quaternion);
    public void ReturnToPool();

}
