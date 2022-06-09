using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Faith
{
    private float _faith;

    public float IndexOfFaith
    {
        get
        {
            if (_faith > 100)
            {
                return 100;
            }
            return _faith;
        }
        set
        {
            _faith = value;
            OnHappinessChange?.Invoke();
            if (_faith < ConstantValues.LowIndexOfHappiness)
            {
                Debug.Log("OnLowHappiness");
                OnLowHappiness?.Invoke();
            }
        }
    }

    public Action OnLowHappiness;
    public Action OnHappinessChange;

    public Faith() : this(Random.Range(35, 70)) { }

    public Faith(int happiness)
    {
        _faith = happiness;
    }
}