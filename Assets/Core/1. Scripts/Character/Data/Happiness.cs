using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Happiness
{
    private float _happiness;

    public float IndexOfHappiness
    {
        get
        {
            if (_happiness > 100)
            {
                return 100;
            }
            return _happiness;
        }
        set
        {
            _happiness = value;
            OnHappinessChange?.Invoke();
            if (_happiness <= ConstantValues.LowIndexOfHappiness)
            {
                Debug.Log("OnLowHappiness");
                OnLowHappiness?.Invoke();
            }
        }
    }

    public Action OnLowHappiness;
    public Action OnHappinessChange;

    public Happiness() : this(Random.Range(35, 70)) { }

    public Happiness(int happiness)
    {
        _happiness = happiness;
    }
}