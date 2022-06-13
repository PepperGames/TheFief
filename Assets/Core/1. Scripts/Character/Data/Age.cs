using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Age
{
    private const float a = 0.00001f;
    private const float b = 0.0002f;
    private const float c = -0.001f;

    public int years = 0;

    public Action OnDeathFromOldAge;

    private int _dayOfBorn = 0;

    public Age() : this(Random.Range(1, 50)) { }

    public Age(int years)
    {
        this.years = years;
        _dayOfBorn = InGameTime.DayInThisYear;
        InGameTime.OnDayChange += OnDayChange;
    }

    private void OnDayChange()
    {
        if (InGameTime.DayInThisYear == _dayOfBorn)
        {
            years++;
            GrowOld();
        }
    }

    private void GrowOld()
    {
        float probabilityToDie = ProbabilityToDie();
        float chance = Random.Range(0, 1f);

        if (probabilityToDie >= chance)
        {
            OnDeathFromOldAge?.Invoke();
        }
    }

    private float ProbabilityToDie()
    {
        float result = (float)(a * Math.Pow(years, 2) + b * years + c);
        return result;
    }
}