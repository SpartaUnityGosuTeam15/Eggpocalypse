using System;
using UnityEngine;

public class Stat
{
    public float CurValue { get; private set; }
    public float MinValue {  get; private set; }
    public float MaxValue {  get; private set; }
    private float _startValue;
    //public float passiveValue;

    public Stat(float startValue, float maxValue, float minValue = 0f)
    {
        _startValue = startValue;
        CurValue = _startValue;
        MaxValue = maxValue;
        MinValue = minValue;
    }

    public void Set(float amount)
    {
        CurValue = Mathf.Clamp(MinValue, amount, MaxValue); ;
    }

    public void Add(float amount)
    {
        CurValue = Mathf.Clamp(MinValue, CurValue + amount, MaxValue);
    }

    public void Subtract(float amount)
    {
        CurValue = Mathf.Clamp(MinValue, CurValue - amount, MaxValue);
    }

    public float GetPercentage()
    {
        return CurValue / MaxValue;
    }
}
