using System;
using UnityEngine;

public class Durability : MonoBehaviour
{
    [SerializeField] private float currentDurability;
    [SerializeField] private float maxDurability;

    public Action<float> OnDurabilityChange;
    public float CurrentDurability
    {
        get { return currentDurability; }

        set
        {
            if (value >= 0)
            {
                currentDurability = value;
                OnDurabilityChange?.Invoke(value);
            }
        }
    }

    public float MaxDurability
    {
        get { return maxDurability; }
        private set { maxDurability = value; }
    }
    public float MissingStrength
    {
        get { return maxDurability - currentDurability; }
    }

    private void Start()
    {
        maxDurability = 100;
        currentDurability = maxDurability;
    }
}
