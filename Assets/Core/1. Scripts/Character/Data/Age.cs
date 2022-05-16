using System;
using Random = UnityEngine.Random;

public class Age
{
    private const float a = 0.00001f;
    private const float b = 0.0002f;
    private const float c = -0.001f;

    public int years = 0;

    public Action OnDeathFromOldAge;

    public Age() : this(Random.Range(1, 50)) { }

    public Age(int years)
    {
        this.years = years;
        InGameTime.OnYearChange += GrowOld;
    }

    private void GrowOld()
    {
        float probabilityToDie = ProbabilityToDie();
        float chance = Random.Range(0, 1f);

        //Debug.Log("chance " + chance);
        if (probabilityToDie >= chance)
        {
            OnDeathFromOldAge?.Invoke();
        }
            //OnDeathFromOldAge?.Invoke();
    }

    private float ProbabilityToDie()
    {
        float result = (float)(a * Math.Pow(years, 2) + b * years + c);
        //Debug.Log("result " + result);
        return result;
    }
}