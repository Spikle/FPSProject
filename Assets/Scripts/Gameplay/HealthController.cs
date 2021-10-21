using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Scripts.Managers;

public class HealthController : MonoBehaviour
{ 
    [SerializeField] protected ValueStats health = new ValueStats(100,100);
    public ValueStats Health => health;

    public Action OnDead;

    public void OnDamage(float damage)
    {
        if (health.Value <= 0)
            return;

        health.RemoveValue(damage);

        if (health.Value <= 0)
            Dead();
    }

    public virtual void Dead()
    {
        OnDead?.Invoke();
        EventManager.OnEndGame?.Invoke();
    }
}

[System.Serializable]
public class ValueStats
{
    public Action<ValueStats> OnChange;
    [SerializeField] private float value = 100f;
    [SerializeField] private float maxValue = 100f;

    public float Value => value;
    public float MaxValue => maxValue;
    public float ValueClapm => Mathf.Clamp(value, 0, maxValue);

    public ValueStats(float maxValue)
    {
        this.value = maxValue;
        this.maxValue = maxValue;
    }

    public ValueStats(float value, float maxValue)
    {
        this.value = value;
        this.maxValue = maxValue;
    }

    public void SetValues(float value, float maxValue)
    {
        this.value = value;
        this.maxValue = maxValue;
        OnChange?.Invoke(this);
    }

    public void AddValue(float value)
    {
        this.value += value;
        this.value = Mathf.Clamp(this.value, this.value, maxValue);
        OnChange?.Invoke(this);
    }

    public void RemoveValue(float value)
    {
        this.value -= value;
        this.value = Mathf.Clamp(this.value, 0, maxValue);
        OnChange?.Invoke(this);
    }

    public void SetMaxValue()
    {
        value = maxValue;
        OnChange?.Invoke(this);
    }
}
