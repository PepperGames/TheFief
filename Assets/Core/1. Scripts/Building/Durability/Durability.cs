using System;
using System.Collections;
using UnityEngine;

public class Durability : MonoBehaviour
{
    [SerializeField] private float _currentDurability;
    [SerializeField] private float _maxDurability;

    [SerializeField] private float _breakdownFrequency;

    public Action<float> OnDurabilityChange;

    public float CurrentDurability
    {
        get { return _currentDurability; }

        set
        {
            if (value >= 0)
            {
                _currentDurability = value;
                OnDurabilityChange?.Invoke(value);
            }
        }
    }

    public float MaxDurability
    {
        get { return _maxDurability; }
        private set { _maxDurability = value; }
    }

    public float MissingStrength
    {
        get { return _maxDurability - _currentDurability; }
    }

    private void Start()
    {
        _maxDurability = 100;
        _currentDurability = _maxDurability;
    }

    public void Break(float percent)
    {
        CurrentDurability -= percent;
    }

    private IEnumerable BreakDownOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(_breakdownFrequency);
            Break(1f);
        }
    }
}
