using System;
using System.Collections;
using UnityEngine;

public class Durability : MonoBehaviour
{
    [SerializeField] private float _currentDurability;
    [SerializeField] private float _maxDurability;

    [SerializeField] private float _breakdownFrequency;
    [SerializeField] private float _currentBreakdownFrequency;

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
        _currentBreakdownFrequency = _breakdownFrequency;

        //StartCoroutine(BreakDownOverTime());
    }

    public void Break(float percent)
    {
        CurrentDurability -= percent;
    }

    private void Update()
    {
        _currentBreakdownFrequency -= Time.deltaTime * InGameSpeed.Speed;
        if (_currentBreakdownFrequency <= 0)
        {
            _currentBreakdownFrequency = _breakdownFrequency;
            Break(1f);
        }
    }

    //private IEnumerator BreakDownOverTime()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(_breakdownFrequency);
    //        _currentBreakdownFrequency = _breakdownFrequency;
    //        Break(1f);
    //    }
    //}

    //private void OnDestroy()
    //{
    //    StopAllCoroutines();
    //}
}
