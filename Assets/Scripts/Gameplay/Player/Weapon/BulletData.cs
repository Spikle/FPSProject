using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BulletData", menuName = "Bullet Data", order = 0)]
public class BulletData : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Bullet prefab;

    public string Name => name;
    public Sprite Sprite => sprite;
    public Bullet Prefab => prefab;
}
