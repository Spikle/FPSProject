using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Text count;

    public void Init(BulletData bulletData)
    {
        Image image = GetComponent<Image>();
        image.sprite = bulletData.Sprite;
    }

    public void UpdateCount(int value)
    {
        count.text = value.ToString();
    }

}
